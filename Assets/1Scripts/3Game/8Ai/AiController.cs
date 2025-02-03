using Photon.Pun;
using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AiController
{
    public static List<AiPlayerSlot> AiPlayers => _aiPlayers;


    private static List<AiPlayerSlot> _aiPlayers;

    public static void AddAiPlayer(PlayerData data)
    {
        if(_aiPlayers == null)
        {
            EventsProvider.OnLevelEnd += ResetStats;
            _aiPlayers = new();
        }

        AiPlayerSlot newAiPlayer = new AiPlayerSlot();

        newAiPlayer.Id = data.userId;
        newAiPlayer.StatsController = new StatsController(data.heroData, OnReducedHealth, false);

        _aiPlayers.Add(newAiPlayer);
    }

    public static StatsController GetPlayerStatsControllerById(string id)
    {
        if (_aiPlayers == null)
        {
            return null;
        }

        foreach (var aiPlayer in _aiPlayers)
        {
            if(aiPlayer.Id == id)
            {
                return aiPlayer.StatsController;
            }
        }

        return null;
    }
      
    public static void OnHeroPrefabCreated(OtherHero otherHero, PlayerData data)
    {
        if(_aiPlayers == null)
        {
            return;
        }

        foreach (var aiPlayer in _aiPlayers)
        {
            if (aiPlayer.Id == data.userId)
            {
                aiPlayer.Hero = otherHero;
                return;
            }
        }
    }

    public static bool TargetIsAi(string id)
    {
        if (_aiPlayers == null)
        {
            return false;
        }

        foreach (var aiPlayer in _aiPlayers)
        {
            if (aiPlayer.Id == id)
            {
                return true;
            }
        }

        return false;
    }

    public static void OnHealRPCRecieved(HealDataRPCSlot data)
    {
        if (_aiPlayers == null)
        { 
            return; 
        }

        for (int i = 0; i < data.Targets.Length; i++)
        {
            for (int j = 0; j < _aiPlayers.Count; i++)
            {
                if (data.Targets[i] == _aiPlayers[j].Id)
                {
                    _aiPlayers[j].StatsController.Heal(data.Value);
                    break;
                }
            }
        }
    }


    public static void OnBuffDataRpcRecieved(BuffDataRPCSlot data)
    {
        if (_aiPlayers == null)
        {
            return;
        }

        BuffSlot buffSlot = new BuffSlot(DataProvider.instance.GetBuff(data.Id), data.Level);

        for (int i = 0; i < data.Targets.Length; i++)
        {
            for (int j = 0; j < _aiPlayers.Count; i++)
            {
                if (data.Targets[i] == _aiPlayers[j].Id)
                {
                    _aiPlayers[j].StatsController.ApplyBuff(buffSlot);
                    break;
                }
            }
        }
    }


    private static void OnReducedHealth(string attackingId, float value, bool fromRpc)
    {
        Debug.Log("#Bot AI Hero Got Damage# " + value + " from " + attackingId + " HP LEFT = " + _aiPlayers[0].StatsController.CurrentHp);

        
        GameMaster.instance.OtherHeroes[0].OnHpReduced(_aiPlayers[0].StatsController.PercentHp, attackingId);
        
    }

    private static void ResetStats()
    {
        foreach (var aiPlayer in _aiPlayers)
        {
            aiPlayer.StatsController.ResetHpMp();
        }
    }

    
}


public class AiPlayerSlot
{
    public string Id;

    public StatsController StatsController;

    public OtherHero Hero;
}