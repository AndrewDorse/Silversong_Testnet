using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampScreenController : ScreenController
{
    private readonly CampScreenView _view;

    public CampScreenController(CampScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        _view.readyButton.onClick.AddListener(ReadyToNextLevel);

       
        

        SubcribeTopButtons();




        EventsProvider.OnPlayersDataChanged += SetPartyMembersUI;
        DataController.instance.AskToUpdatePlayersData();
    }

    private void ReadyToNextLevel()
    {
        if (MainRPCController.instance.IsMaster)
        {
            DataController.instance.SetLocalPlayerReady();
        }
        else
        {
            MainRPCController.instance.SetLocalPlayerReady();
        }

    }

    private void SetPartyMembersUI(List<PlayerData> data)
    {
        for (int i = 0; i < _view.campHeroSlots.Length; i++)
        {
            if(i < data.Count)
            {
                _view.campHeroSlots[i].gameObject.SetActive(true);
                _view.campHeroSlots[i].Setup(data[i], OnPartyMemberClick);
            }
            else
            {
                _view.campHeroSlots[i].gameObject.SetActive(false);
            }

        }


    }

    private void OnPartyMemberClick() // open popup with player??? / hero!!! info
    {

    }




    private void SubcribeTopButtons()
    {
        _view.botButtons[0].Setup(OpenCamp);
        _view.botButtons[1].Setup(OpenInventory);
        _view.botButtons[2].Setup(OpenAbilities);
    }


    private void OpenCamp()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.camp);
    }

    private void OpenInventory()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.inventory);
    }

    private void OpenAbilities()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.abilities);
    }



    public override void Dispose()
    {
        base.Dispose();
        EventsProvider.OnPlayersDataChanged -= SetPartyMembersUI;
    }

}
