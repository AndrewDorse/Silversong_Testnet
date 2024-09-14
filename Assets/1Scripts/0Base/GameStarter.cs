using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter 
{
    
    public void Initialize()
    {
        EventsProvider.OnPlayersDataChanged += CheckIfAllPlayersAreReadyForStartGame;


        EventsProvider.OnLevelEnd += SubcribeForNewLevelStart;

    }


    private void SubcribeForNewLevelStart()
    {
        EventsProvider.OnPlayersDataChanged += CheckIfAllPlayersAreReadyForStartNewLevel;
    }


    private void CheckIfAllPlayersAreReadyForStartGame(List<PlayerData> playersData)
    {
        if (DataController.instance.GameData.gameStage != Enums.ServerGameStage.creatingHeroes)
        {
            return;
        }

        if (!IsAllPlayersReady(playersData))
        {
            return;
        }
        
        StartGame();
    }



    private void CheckIfAllPlayersAreReadyForStartNewLevel(List<PlayerData> playersData)
    {
        if (DataController.instance.GameData.gameStage != Enums.ServerGameStage.camp)
        {
            return;
        }

        if (!IsAllPlayersReady(playersData))
        {
            return;
        }

        StartNewLevel();
    }

    private bool IsAllPlayersReady(List<PlayerData> playersData)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }

        if (playersData.Count == 0)
        {
            return false;
        }

        foreach (PlayerData player in playersData)
        {
            if (player.ready == false)
            {
                return false;
            }
        }

        return true;
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DataController.instance.GameData.gameStage = Enums.ServerGameStage.gameLevel;
            DataController.instance.GameData.sceneName = GetNextSceneName(DataController.instance.GameData.Level);



            EventsProvider.OnPlayersDataChanged -= CheckIfAllPlayersAreReadyForStartGame;

            EventsProvider.OnGameStart?.Invoke();

            MainRPCController.instance.SendStartGameInfo();

            Master.instance.ChangeGameStage(Enums.GameStage.game);

        }
    }

    private void StartNewLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DataController.instance.GameData.gameStage = Enums.ServerGameStage.gameLevel;
            DataController.instance.GameData.sceneName = GetNextSceneName(DataController.instance.GameData.Level);
            EventsProvider.OnPlayersDataChanged -= CheckIfAllPlayersAreReadyForStartNewLevel;


            MainRPCController.instance.SendStartNewLevel();
        }
    }

    private string GetNextSceneName(int level)
    {
        return "ForestGlade_2_VIolet";

    }
    


}
