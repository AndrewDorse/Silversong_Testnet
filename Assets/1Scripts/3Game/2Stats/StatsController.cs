using Silversong.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StatsController
{

    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }

    private StatsHolder _stats;
    private Action<string, float, bool> _onHpReducedCallback;

    private BuffDebuffController _buffDebuffController;
    private CharacterStatesController _statesController;
    private PassiveController _passiveController;


    public StatsController(HeroData heroData) // only once for local player
    {
        HeroClass heroClass = InfoProvider.instance.GetHeroClass(heroData.classId);
        Subrace subrace = InfoProvider.instance.GetSubrace(heroData.SubraceId);


        _stats = new StatsHolder(heroClass.attribute);

        Action<Enums.ControlState, bool, bool, bool, bool> controlStatesCallback = null;

        _statesController = new CharacterStatesController(controlStatesCallback);

        _buffDebuffController = new BuffDebuffController(_statesController.OnControlStatesChanged, this);

        _passiveController = new PassiveController(heroData.PassiveAbilities, PassiveTriggered);

        EventsProvider.OnLocalHeroPassiveTrigger += (Enums.PassiveTrigger trigger) =>
        {
            _passiveController.CheckPassivesByTrigger(trigger);
        };

        // apply hero class and subrace
        // ApplyStatList(heroClass.stats on choose);



        EventsProvider.OnLevelStart += OnLevelStart;

        SubcribeLocalHero();
    }



    public StatsController(EnemyData enemyData,
        Action<string, float, bool> onHpChangeCallback,
        Action<Enums.ControlState,bool, bool, bool,bool> controlStatesCallback)
    {
        _stats = new StatsHolder();

        CurrentHp = _stats.stats[0];
        CurrentMp = _stats.stats[2];

        _onHpReducedCallback = onHpChangeCallback;


        _statesController = new CharacterStatesController(controlStatesCallback);
        _buffDebuffController = new BuffDebuffController(_statesController.OnControlStatesChanged, this);
        //_passiveController = new PassiveController();

        // apply enemy stats list + level increase
    }



    private void OnLevelStart()
    {
        _passiveController.UpdatePassivesInfo(DataController.LocalPlayerData.heroData.PassiveAbilities);
    }




    public float GetStat(Enums.Stats stat)
    {
        return _stats.stats[(int)stat];
    }

    public void ReduceHealth(string attackingId, float damage)
    {
        CurrentHp -= damage;

        _onHpReducedCallback?.Invoke(attackingId, damage, false);
    }

    public void ReduceHealthFromRpc(float damage)
    {
        CurrentHp -= damage;
        _onHpReducedCallback?.Invoke("", damage, true);
    }

    public void CurrentHpInfoFromMaster(float value)
    {
        CurrentHp = value;
    }


    


    private void PassiveTriggered(PassiveAbilitySlot passiveAbilitySlot)
    {

        Debug.Log("#PassiveTriggered# " + passiveAbilitySlot.PassiveAbility.Type);

        if (passiveAbilitySlot.PassiveAbility.Type == Enums.PassiveTypes.buffByTrigger)
        {
            ApplyBuff(new BuffSlot(passiveAbilitySlot.PassiveAbility.Buff, passiveAbilitySlot.Level));
        }
    }















    private void ChangeStat(int statId, float value) // + value
    {
        _stats.stats[statId] += value;
    }




    private void ApplyStatList(List<StatSlot> list, Enums.StatType type)
    {
        int offset = GetOffsetAmount(type);

        foreach (StatSlot slot in list)
        {
            ChangeStat((int)slot.stat + offset, slot.value);
            Debug.Log("#Change STAT " + slot.stat + " on " + slot.value);
        }

        _stats.CalculateStats();
    }

    private int GetOffsetAmount(Enums.StatType type)
    {
        int result = 100;

        if (type == Enums.StatType.basic)
        {
            result = 100;
        }
        else if (type == Enums.StatType.passive)
        {
            result = 200;
        }
        else if (type == Enums.StatType.item)
        {
            result = 300;
        }
        else if (type == Enums.StatType.buff)
        {
            result = 400;
        }

        return result;
    }










    // buff debuff
    public void ApplyBuff(BuffSlot slot)
    {
        _buffDebuffController.ApplyBuff(slot);
    }

    public void ApplyBuffStats(List<StatSlot> list)
    {
        ApplyStatList(list, Enums.StatType.buff);
    }

    public void RemoveBuffStats(List<StatSlot> list, int stack)
    {
        int offset = GetOffsetAmount(Enums.StatType.buff);

        foreach (StatSlot slot in list)
        {
            ChangeStat((int)slot.stat + offset, - slot.value * stack);
            Debug.Log("#Change STAT " + slot.stat + " on " + - slot.value * stack);
        }

        _stats.CalculateStats();
    }




    
    private void SubcribeLocalHero()
    {
        EventsProvider.OnPassiveAbilityLearnt += AddPassiveAbilityStats;
        EventsProvider.OnInventoryItemsAdded += AddItemStats;



    }



    // learned passives
    private void AddPassiveAbilityStats(PassiveAbility passive)
    {
        if(passive.Type == Enums.PassiveTypes.stats)
        {
            ApplyStatList(passive.LevelInfo[0].Stats, Enums.StatType.passive);
        }
    }

    private void AddItemStats(ItemSlot itemSlot)
    {
        InventoryItem item = InfoProvider.instance.GetItem(itemSlot.Id);

        if (item.Stats.Count > 0)
        {
            ApplyStatList(item.Stats, Enums.StatType.item);
        }
    }

}




    



[System.Serializable]
public class StatSlot
{
    public Enums.Stats stat;
    public float value;
}