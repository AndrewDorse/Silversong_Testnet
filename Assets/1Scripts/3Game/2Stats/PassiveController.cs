using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveController 
{


    private List<PassiveAbilitySlot> _passives;


    private Action<PassiveAbilitySlot> _callback;


    public PassiveController(List<PassiveAbilityDataSlot> data, Action<PassiveAbilitySlot> callback)
    {
        SetTriggerPassives(data);

        _callback = callback;

    }


    public void UpdatePassivesInfo(List<PassiveAbilityDataSlot> data)
    {
        SetTriggerPassives(data);
    }


    private void SetTriggerPassives(List<PassiveAbilityDataSlot> passives)
    {
        _passives = new List<PassiveAbilitySlot>();

        foreach (PassiveAbilityDataSlot slot in passives)
        {
            PassiveAbility passiveAbility = DataProvider.instance.GetPassive(slot.Id);


            if (passiveAbility.Type == Enums.PassiveTypes.buffByTrigger ||
                passiveAbility.Type == Enums.PassiveTypes.castByTrigger ||
                passiveAbility.Type == Enums.PassiveTypes.debuffToEnemyByTrigger ||
                passiveAbility.Type == Enums.PassiveTypes.healByTrigger ||
                passiveAbility.Type == Enums.PassiveTypes.permanentStatsByTrigger
                )
            {
                _passives.Add(new PassiveAbilitySlot(passiveAbility, slot.Level));
            }
        }
    }

    public void CheckPassivesByTrigger(Enums.PassiveTrigger passiveTrigger)
    {
        Debug.Log("#CheckPassivesByTrigger# " + passiveTrigger);

        foreach (PassiveAbilitySlot slot in _passives)
        {
            if (slot.Cooldown > 0)
            {
                continue;
            }

            if (slot.PassiveAbility.Trigger == passiveTrigger)
            {
                TriggerPassive(slot);
            }
        }



    }







    private void TriggerPassive(PassiveAbilitySlot passiveSlot)
    {
        if(passiveSlot.PassiveAbility.Type == Enums.PassiveTypes.buffByTrigger)
        {

            _callback?.Invoke(passiveSlot);

        }
    }




















    public void Dispose()
    {
        //EventsProvider.OnLocalHeroPassiveTrigger -= CheckPassivesByTrigger;
    }


    
}

[System.Serializable]
public class PassiveAbilitySlot
{
    public PassiveAbility PassiveAbility;
    public int Level;
    public float Cooldown;

    public PassiveAbilitySlot(PassiveAbility passiveAbility, int level = 1)
    {
        this.PassiveAbility = passiveAbility;
        this.Level = level;
    }
}


