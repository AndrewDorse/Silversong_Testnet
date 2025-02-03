using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalHeroAbilityCaster 
{

    private ActiveAbilitySlot _abilityToCast;



    public LocalHeroAbilityCaster ()
    {
        EventsProvider.OnAbilityButtonPressed += AbilityButtonPressed;

        EventsProvider.OnAbilityButtonReleased += AbilityButtonReleased;
        EventsProvider.OnAbilityUseTrigger += UseAbility;
    }



    private void AbilityButtonPressed(int buttonId)
    {
        // check if its in cooldown??? TODO

        ActiveAbilityDataSlot abilityDataSlot = DataController.LocalPlayerData.heroData.activeTalents[buttonId];


        _abilityToCast = new ActiveAbilitySlot(DataProvider.instance.GetAbility(abilityDataSlot.Id), abilityDataSlot.Level);


        ApplyAbilityCost(_abilityToCast);


        if(_abilityToCast.activeAbility.CastType == Enums.AbilityCastTypes.withoutTarget)
        {
            //start cast
            if(_abilityToCast.activeAbility.LevelInfo[_abilityToCast.level- 1].CastTime <= 0.5f)// cast immediately
            {
                EventsProvider.OnAbilityCastFinished?.Invoke(_abilityToCast);
            }
            else
            {
                EventsProvider.OnAbilityCastStarted?.Invoke();
            }

        }
        else if (_abilityToCast.activeAbility.CastType == Enums.AbilityCastTypes.pointing)
        {
            //start cast while we're pointing
        }


    }

    private void UseAbility()
    {
        FightCalculation.CalculateSpell(GameMaster.instance.LocalHero, _abilityToCast);
    }



    private void AbilityButtonReleased(int buttonId)
    {



    }


    private bool ApplyAbilityCost(ActiveAbilitySlot slot)
    {
        // check if we have enough mana/health

        return true;
    }







}



[System.Serializable]
public class ActiveAbilitySlot
{
    public ActiveAbility activeAbility;
    public int level;

    public ActiveAbilitySlot(ActiveAbility activeAbility, int level = 1)
    {
        this.activeAbility = activeAbility;
        this.level = level;
    }
}