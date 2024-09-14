using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenController : ScreenController
{
    private readonly GameScreenView _view;

    public GameScreenController(GameScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();


        SetupAbilityButtons(DataController.LocalPlayerData.heroData.activeTalents);
    }


    private void SetupAbilityButtons(List<ActiveAbilityDataSlot> abilitySlots)
    {
        for(int i = 0; i < _view.abilityButtons.Length; i++)
        {
            if(i < abilitySlots.Count)
            {
                _view.abilityButtons[i].gameObject.SetActive(true);
                _view.abilityButtons[i].Setup(InfoProvider.instance.GetAbility(abilitySlots[i].Id), AbilityButtonPressed, AbilityButtonReleased, i);
            }
            else
            {
                _view.abilityButtons[i].gameObject.SetActive(false);
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

}
