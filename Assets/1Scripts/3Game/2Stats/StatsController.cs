using Silversong.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class StatsController
{ 
    public float CurrentHp { get; private set; }
    public float CurrentMp { get; private set; }
    public float PercentHp => CurrentHp / GetStat(Enums.Stats.hp);
    public float PercentMp => CurrentMp / GetStat(Enums.Stats.mp);
    public Transform CharacterTransform => _characterTransform;



    private StatsHolder _statsHolder;
    private Action<string, float, bool> _onHpReducedCallback;

    private BuffDebuffController _buffDebuffController;
    private CharacterStatesController _statesController;
    private PassiveController _passiveController;

    private Transform _characterTransform;

    public StatsController(HeroData heroData, Action<string, float, bool> onReducedHealth, bool localHero = true) // local hero
    {
        HeroClass heroClass = DataProvider.HeroClassProviderData.GetHeroClass(heroData.classId);
        Subrace subrace = DataProvider.instance.GetSubrace(heroData.SubraceId);
         

        _statsHolder = new StatsHolder(heroClass.attribute); 


        Action<Enums.ControlState, bool, bool, bool, bool> controlStatesCallback = null;
         
        ApplyStatList(heroClass.startStats, Enums.StatType.basic);

        _onHpReducedCallback = onReducedHealth;

        EventsProvider.OnLevelStart += OnLevelStart;

        if (localHero)
        {
            SubcribeLocalHero();

            _statesController = new CharacterStatesController(controlStatesCallback);
            _buffDebuffController = new BuffDebuffController(_statesController.OnControlStatesChanged, this);
            _passiveController = new PassiveController(heroData.PassiveAbilities, PassiveTriggered);

            EventsProvider.OnLocalHeroPassiveTrigger += (Enums.PassiveTrigger trigger) =>
            {
                _passiveController.CheckPassivesByTrigger(trigger);
            };
        } 
        else
        {
            _statesController = new CharacterStatesController(controlStatesCallback);
            _buffDebuffController = new BuffDebuffController(_statesController.OnControlStatesChanged, this);
            _passiveController = new PassiveController(heroData.PassiveAbilities, PassiveTriggered);
        }

        _statsHolder.CalculateStats();
        CurrentHp = GetStat(Enums.Stats.hp); 
    }

    

    public StatsController(EnemyData enemyData,
        Action<string, float, bool> onHpChangeCallback,
        Action<Enums.ControlState,bool, bool, bool,bool> controlStatesCallback, Transform transform)
    {
        _statsHolder = new StatsHolder();

        

        _onHpReducedCallback = onHpChangeCallback;


        _characterTransform = transform;


        _statesController = new CharacterStatesController(controlStatesCallback);
        _buffDebuffController = new BuffDebuffController(_statesController.OnControlStatesChanged, this);
        //_passiveController = new PassiveController();

        // apply enemy stats list + level increase

        Mob mobData = DataProvider.MobProviderData.GetMob(enemyData.mobId);

        ApplyStatList(mobData.Stats, Enums.StatType.basic);

        ResetHpMp();
    }

    public void ResetHpMp()
    {
        CurrentHp = _statsHolder.stats[0];
        CurrentMp = _statsHolder.stats[2];
    }

    private void OnLevelStart()
    {
        ResetHpMp();
        _passiveController.UpdatePassivesInfo(DataController.LocalPlayerData.heroData.PassiveAbilities);
    }




    public float GetStat(Enums.Stats stat)
    {
        return _statsHolder.stats[(int)stat];
    }

    public void ReduceHealth(string attackingId, float damage)
    {
        CurrentHp -= damage;

        _onHpReducedCallback?.Invoke(attackingId, damage, false);
    }

    public void Heal(float value)
    {
        CurrentHp += value;

        Debug.Log("###HEAL + value");

        if(CurrentHp > GetStat(Enums.Stats.hp))
        {
            CurrentHp = GetStat(Enums.Stats.hp);
        } 
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

    public void UpdateTransform(Transform transform)
    {
        _characterTransform = transform;
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
        _statsHolder.stats[statId] += value;
    }




    private void ApplyStatList(List<StatSlot> list, Enums.StatType type)
    {
        int offset = GetOffsetAmount(type);

        foreach (StatSlot slot in list)
        {
            ChangeStat((int)slot.stat + offset, slot.value);
            Debug.Log("#Change STAT " + slot.stat + " on " + slot.value);
        }

        _statsHolder.CalculateStats();
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
        _buffDebuffController.ApplyBuff(slot, _characterTransform);
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

        _statsHolder.CalculateStats();
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
        InventoryItem item = DataProvider.instance.GetItem(itemSlot.Id);

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