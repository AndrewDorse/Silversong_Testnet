using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatsController : MonoBehaviour
{
    public static HeroStatsController instance;

    

    public StatsController StatsController { get => _statsController; private set => _statsController = value; }

    private StatsController _statsController;
    private HeroDamageDealer _heroDamageDealer;
    private LocalHeroAbilityCaster _abilityCaster;



    private void Awake()
    {
        instance = this;

        EventsProvider.OnGameStart += Initialize;
        EventsProvider.OnLevelEnd += OnLevelEnd;
        EventsProvider.OnHealDataRpcRecieved += OnHealRPCRecieved;
        EventsProvider.OnBuffDataRpcRecieved += OnBuffDataRpcRecieved;
    }

    private void Initialize()
    {
        HeroData heroData = DataController.instance.GetMyHeroData();

        _heroDamageDealer = new HeroDamageDealer();

        _statsController = new StatsController(heroData, OnReducedHealth);


        _abilityCaster = new LocalHeroAbilityCaster();
    }


    private void OnLevelEnd()
    {
        if(_statsController.CurrentHp > 0)
        {
           
        }

        LevelUp();
    }

    private void LevelUp()
    {
        DataController.LocalPlayerData.heroData.level++;
        DataController.LocalPlayerData.heroData.TalentPoints += 33;

        Debug.Log("#LevelUp# " + DataController.LocalPlayerData.heroData.level);
    }


    private void OnReducedHealth(string attackingId, float value, bool fromRpc)
    {
        EventsProvider.OnLocalHeroHpMpChange?.Invoke(StatsController.PercentHp, StatsController.PercentMp);

        Debug.Log("#Hero Got Damage# " + value + " from " + attackingId + " HP LEFT = " + StatsController.CurrentHp);
    }

    private void OnHealRPCRecieved(HealDataRPCSlot data)
    { 
        for (int i = 0; i < data.Targets.Length; i++)
        {
            if(data.Targets[i] == DataController.LocalPlayerData.userId)
            {
                _statsController.Heal(data.Value);
            }
        }

        AiController.OnHealRPCRecieved(data);
    }


    private void OnBuffDataRpcRecieved(BuffDataRPCSlot data)
    { 
        BuffSlot buffSlot = new BuffSlot(DataProvider.instance.GetBuff(data.Id), data.Level);

        for (int i = 0; i < data.Targets.Length; i++)
        {
            if (data.Targets[i] == DataController.LocalPlayerData.userId)
            {
                _statsController.ApplyBuff(buffSlot);
                break;
            }
        }

        AiController.OnBuffDataRpcRecieved(data);
    }
}
