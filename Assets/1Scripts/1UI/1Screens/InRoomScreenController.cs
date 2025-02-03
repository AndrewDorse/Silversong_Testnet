using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoomScreenController : ScreenController
{
    private readonly InRoomScreenView _view;

    public InRoomScreenController(InRoomScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        _view.heroCreateButton.onClick.AddListener(StartHeroCreation);
        _view.leaveRoomButton.onClick.AddListener(LeaveRoom);
        _view.readyButton.onClick.AddListener(Ready);
        _view.addBotButton.onClick.AddListener(AddBot);

        EventsProvider.OnPlayersDataChanged += UpdatePlayerList; 
        DataController.instance.AskToUpdatePlayersData();
    }

    private void StartHeroCreation()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.heroCreation);
    }

    private void LeaveRoom()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.menu);
    }

    private void Ready()
    {
        MainRPCController.instance.SetLocalPlayerReady();
    }

    private void AddBot()
    {
        DataController.instance.TryAddBot();
    }

    private void UpdatePlayerList(List<PlayerData> info)
    {
        Debug.Log("#IN ROOM# UpdatePlayerList " + info.Count) ;
          
        for (int i = 0; i < _view.playerSlotsInRoom.Length; i++)
        {
            if (i < info.Count)
            {
                _view.playerSlotsInRoom[i].gameObject.SetActive(true);
                _view.playerSlotsInRoom[i].Setup(info[i]);
            }
            else
            {
                _view.playerSlotsInRoom[i].gameObject.SetActive(false);
            }
        }
    }





    public override void Dispose()
    {
        base.Dispose();

        EventsProvider.OnPlayersDataChanged -= UpdatePlayerList;
    }


}
