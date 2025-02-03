using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class PhotonConnector : MonoBehaviourPunCallbacks
{
	private string _gameVersion;
	private bool _isConnecting;
	[SerializeField] private bool _isReconnecting;
	private Region _region;


    private void Start()
    {
		InitialConnect();
	}

	private void InitialConnect()
	{
		PhotonNetwork.AutomaticallySyncScene = false;
		_gameVersion = "0.01";

		_isConnecting = true;

		if (PhotonNetwork.IsConnected)
		{

			RegionConnect();
		}
		else
		{
			PhotonNetwork.NetworkingClient.AppId = "4280e39a-61ea-40ee-9008-2a8aec932533";

			PhotonNetwork.NetworkingClient.ConnectToNameServer();

			PhotonNetwork.GameVersion = _gameVersion;
		}
	}

	public void Connect()
	{
		if (PhotonNetwork.IsConnected)
		{
			RegionConnect();
		}
		else
		{
			InitialConnect();
		}
	}

	private void RegionConnect()
    {
		PhotonNetwork.NetworkingClient.ConnectToRegionMaster(DataController.instance.LocalData.regionCode);


		PhotonNetwork.NickName = "Andrew Android" + Random.Range(0,10000);

#if UNITY_EDITOR
		PhotonNetwork.NickName = "Andrew Desktop" + Random.Range(0, 10000);
#endif


		

	}

	public void StartQuickGame()
    {
		if (PhotonNetwork.IsConnectedAndReady == false) return; //?

		Master.instance.SetLightLoadingScreen();

        PhotonNetwork.JoinRandomOrCreateRoom(null, 
			5, 
			MatchmakingMode.FillRoom, 
			null, 
			null, 
			null, 
			new RoomOptions { MaxPlayers = 5, PublishUserId = true, IsVisible = true });



    }

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}












	// Callbacks
	public override void OnConnectedToMaster()
	{
		Debug.Log("#pun# OnConnectedToMaster");
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		Debug.Log("#pun# OnJoinedLobby");

		// reconnect
		if(DataController.instance.LocalData.RoomId.Length > 15)
        {
			// timer???
			_isReconnecting = true;


			PhotonNetwork.JoinRoom(DataController.instance.LocalData.RoomId);

		}
        else
        {
			_isReconnecting = false;

		}

	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		Debug.Log("#pun# OnRoomListUpdate rooms count " + roomList.Count);
	}

	public override void OnConnected()
	{
		Debug.Log("#pun# OnConnected region " + PhotonNetwork.NetworkingClient.CurrentCluster);

		Master.instance.OnPhotonConnection();
		//DataController.instance.SetPlayerData(new PlayerData(PhotonNetwork.LocalPlayer));
		DataController.instance.LocalData.UserId = PhotonNetwork.LocalPlayer.UserId;
	}

	public override void OnRegionListReceived(RegionHandler regionHandler)
	{
		Debug.Log("#pun# OnRegionListReceived");		
	}

    public override void OnJoinedRoom()
    { 
		




		//reconnect
		if (_isReconnecting)
		{
			Debug.Log("#pun# OnJoinedRoom RECONNECT" );
			MainRPCController.instance.GetPlayersDataFromMaster();
			EventsProvider.OnGameDataRpcRecieved += OnRecievedGameDataForReconnect;
		}
		else // first connect
		{
			Debug.Log("#pun# OnJoinedRoom NORMAL");
			 
			EventsProvider.OnGameDataRpcRecieved -= OnRecievedGameDataForReconnect;
			 
			EventsProvider.OnJoinRoom?.Invoke();

			DataController.instance.LocalData.OldUserId = PhotonNetwork.LocalPlayer.UserId; // for reconnect

			DataController.instance.LocalData.RoomId = PhotonNetwork.CurrentRoom.Name;
			SaveController.SaveLocalData();



            Master.instance.ChangeGameStage(Enums.GameStage.inRoom);

            if (PhotonNetwork.IsMasterClient)
			{
				DataController.instance.GameData.roomId = PhotonNetwork.CurrentRoom.Name;
				DataController.instance.TryToAddPlayer(PhotonNetwork.LocalPlayer);
			}
			else
			{
				MainRPCController.instance.GetPlayersDataFromMaster();
			}


        }

		

		
	}

    public override void OnLeftRoom()
    {
		Debug.Log("#pun# OnLeftRoom");
		EventsProvider.OnLeftRoom?.Invoke();
	}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
		EventsProvider.OnOtherPlayerEnteredRoom?.Invoke(newPlayer);

		Debug.Log("#pun# OnPlayerEnteredRoom");
	}

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
		EventsProvider.OnOtherPlayerLeftRoom?.Invoke(otherPlayer);
	}

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
		Debug.Log("#pun# OnCreatedRoom");


	}
	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		base.OnJoinRoomFailed(returnCode, message);

		Debug.Log("#pun# OnJoinRoomFailed " + message);

		_isReconnecting = false;
	}

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);

		if (newMasterClient.UserId == PhotonNetwork.LocalPlayer.UserId)
        {
			EventsProvider.OnBecomeMaster?.Invoke();
        }
    }






    // reconnect

    private void OnRecievedGameDataForReconnect(PlayersDataRPC data)
    {
		EventsProvider.OnGameDataRpcRecieved -= OnRecievedGameDataForReconnect;

		Debug.Log("#pun# OnRecievedGameDataForReconnect  " + JsonUtility.ToJson(data));

		Enums.GameStage stage = Enums.GameStage.camp;


		if (data.gameData.started)
		{
			if (data.gameData.gameStage == Enums.ServerGameStage.gameLevel)
			{
				stage = Enums.GameStage.game;
			}
			else if (data.gameData.gameStage == Enums.ServerGameStage.camp)
			{
				stage = Enums.GameStage.camp;
			}
		}
		else
        {
			stage = Enums.GameStage.inRoom;
		}



		MainRPCController.instance.SendNewUserIdToOthers();

		DataController.instance.LocalData.OldUserId = PhotonNetwork.LocalPlayer.UserId; 
		SaveController.SaveLocalData();

		Master.instance.ChangeGameStage(stage);
	}

    
}
