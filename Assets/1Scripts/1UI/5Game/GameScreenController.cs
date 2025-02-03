using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameScreenController : ScreenController
{
    private readonly GameScreenView _view;

    public GameScreenController(GameScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();


        SetupAbilityButtons(DataController.LocalPlayerData.heroData.activeTalents);

        _view.statsButton.onClick.AddListener(StatsClick);
         

        DataController.instance.AskToUpdatePlayersData();

        PartyPlayersSetup(DataController.AllPlayerData);

        EventsProvider.OnOtherHeroInfoRpcRecieved += UpdatePlayerInfo;
        EventsProvider.OnLocalHeroHpMpChange += SetLocalHeroHpMp;
    }


    private void SetupAbilityButtons(List<ActiveAbilityDataSlot> abilitySlots)
    {
        for(int i = 0; i < _view.abilityButtons.Length; i++)
        {
            if(i < abilitySlots.Count)
            {
                _view.abilityButtons[i].gameObject.SetActive(true);
                _view.abilityButtons[i].Setup(DataProvider.instance.GetAbility(abilitySlots[i].Id), AbilityButtonPressed, AbilityButtonReleased, i);
            }
            else
            {
                _view.abilityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void UpdatePlayerInfo(HeroInfoRPC data)
    {
        for (int i = 0; i < _view.partyPlayerSlotUIs.Length; i++)
        {
            if (_view.partyPlayerSlotUIs[i].Id == data.userId)
            {
                _view.partyPlayerSlotUIs[i].SetHpMp(data.healthPc, data.manaPc);
            } 
        }
    }


    private void PartyPlayersSetup(List<PlayerData> data)
    {
        int iter = 0;

        for (int i = 0; i < _view.partyPlayerSlotUIs.Length; i++)
        {
            if (i < data.Count)
            {
                if (data[i].userId == DataController.LocalPlayerData.userId)
                {
                    iter++; 
                    _view.partyPlayerSlotUIs[i].gameObject.SetActive(false);
                    continue;
                }

                _view.partyPlayerSlotUIs[i].Setup(data[i]); 
            }
            else
            {
                _view.partyPlayerSlotUIs[i].gameObject.SetActive(false) ;
            }
        }
    }

    private void AbilityButtonPressed(int buttonId)
    {
        EventsProvider.OnAbilityButtonPressed?.Invoke(buttonId);
    }

    private void AbilityButtonReleased(int buttonId)
    {
        EventsProvider.OnAbilityButtonReleased?.Invoke(buttonId);
    }

    private void StatsClick()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.heroStats);
    }
     
    private void SetLocalHeroHpMp(float hp, float mp)
    {
        _view.hpImage.fillAmount = hp;
        _view.mpImage.fillAmount = mp;
    }

    public override void Dispose()
    {
        base.Dispose();
        EventsProvider.OnOtherHeroInfoRpcRecieved -= UpdatePlayerInfo;
        EventsProvider.OnLocalHeroHpMpChange -= SetLocalHeroHpMp;
    }

}
