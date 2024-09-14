using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class MainRPCController : MonoBehaviourPun
{
    public static MainRPCController instance;

    public bool IsMaster => PhotonNetwork.IsMasterClient;



    private void Awake()
    {
        instance = this;
        EventsProvider.OnPlayersDataChanged += SendPlayersDataToOthers;
    }


   




    // players data
    public void GetPlayersDataFromMaster()
    {
        photonView.RPC("GetPlayersDataRPC", RpcTarget.MasterClient);
    }

    private void SendPlayersDataToOthers(List<PlayerData> data)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetPlayersDataRPC", RpcTarget.Others, DataController.instance.GetPlayersDataForRPC());
        }
    }

    [PunRPC]
    private void GetPlayersDataRPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetPlayersDataRPC", RpcTarget.Others, DataController.instance.GetPlayersDataForRPC());
        }
    }

    [PunRPC]
    private void SetPlayersDataRPC(string data)
    {
        Debug.Log("#RPC# SetPlayersDataRPC + " + data);

        PlayersDataRPC playersData = JsonUtility.FromJson<PlayersDataRPC>(data);

        DataController.instance.SetGameData(playersData.allPlayersData, playersData.gameData);

        EventsProvider.OnGameDataRpcRecieved?.Invoke(playersData);


     
    }


    // hero data
    public void SendHeroDataToMaster()
    {
        photonView.RPC("SendHeroDataToMasterRPC", RpcTarget.MasterClient, DataController.instance.GetMyHeroDataForRPC());
    }

    [PunRPC]
    private void SendHeroDataToMasterRPC(string data)
    {
        Debug.Log("#RPC# SendHeroDataToMasterRPC");

        HeroDataRPC playersData = JsonUtility.FromJson<HeroDataRPC>(data);
        DataController.instance.ChangeHeroDataByUserId(playersData.heroData, playersData.UserId);
    }


    // in room
    public void SetLocalPlayerReady()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DataController.instance.SetLocalPlayerReady();
        }
        else
        {
            photonView.RPC("SendReadyToMasterRPC", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId);
        }
    }

    [PunRPC]
    private void SendReadyToMasterRPC(string userId)
    {
        DataController.instance.SetPlayerReady(userId);
    }

    // start game
    public void SendStartGameInfo()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DataController.instance.AskToUpdatePlayersData();

            photonView.RPC("StartGameRPC", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void StartGameRPC()
    {
        if (PhotonNetwork.IsMasterClient == false)
        { 
            EventsProvider.OnGameStart?.Invoke();
        }

        Master.instance.ChangeGameStage(Enums.GameStage.game);
    }


    public void SendStartNewLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            DataController.instance.AskToUpdatePlayersData();
            

            photonView.RPC("SendStartNewLevelRPC", RpcTarget.Others, DataController.instance.GetPlayersDataForRPC());

            Master.instance.ChangeGameStage(Enums.GameStage.game);
        }
    }
    [PunRPC]
    private void SendStartNewLevelRPC(string data)
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            PlayersDataRPC playersData = JsonUtility.FromJson<PlayersDataRPC>(data);
            DataController.instance.GameData = playersData.gameData;
            Master.instance.ChangeGameStage(Enums.GameStage.game);
        }
    }








        // RECONNECT

        public void SendNewUserIdToOthers()
    {
        photonView.RPC("SendNewUserIdToOthers", RpcTarget.Others, JsonUtility.ToJson(new NewUserIdDataRpc(
            DataController.instance.LocalData.UserId,
            DataController.instance.LocalData.OldUserId)));
    }

    [PunRPC]
    private void SendNewUserIdToOthers(string data)
    {
        EventsProvider.OnOtherPlayerReconnect?.Invoke(JsonUtility.FromJson<NewUserIdDataRpc>(data));
    }

}


[System.Serializable]
public class NewUserIdDataRpc
{
    public string UserId;
    public string OldUserId;

    public NewUserIdDataRpc(string UserId, string OldUserId)
    {
        this.UserId = UserId;
        this.OldUserId = OldUserId;
    }
}
