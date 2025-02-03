using Silversong.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuffController
{
    private List<BuffSlot> _buffs = new List<BuffSlot>();
    private List<BuffSlot> _debuffs = new List<BuffSlot>();

    private List<StateSlot> _controlSlots = new List<StateSlot>();


    private Action<List<StateSlot>> _onChangedControlSlots;

    private StatsController _statsController;



    public BuffDebuffController(Action<List<StateSlot>> onChangeControlSlots, StatsController statsController)
    {
        _onChangedControlSlots = onChangeControlSlots;

        EventsProvider.TenTimesPerSecond += TimeTick;

        _statsController = statsController;
    }


    private void TimeTick() // 0.1f
    {
        for (int i = 0; i < _buffs.Count; i++)
        {
            _buffs[i].duration -= 0.1f;
            if (_buffs[i].duration <= 0)
            {
                RemoveBuff(_buffs[i]);
                i--;
            }
        }

        for (int i = 0; i < _debuffs.Count; i++)
        {
            _debuffs[i].duration -= 0.1f;
            if (_debuffs[i].duration <= 0)
            {
                RemoveBuff(_debuffs[i]);
                i--;
            }
        }
    }

    private void RemoveBuff(BuffSlot buffSlot)
    {
        RemoveBuffStats(buffSlot);

        if (buffSlot.effect != null) buffSlot.effect.SetActive(false);

        if (buffSlot.ControlSlot != null) _controlSlots.Remove(buffSlot.ControlSlot);



        if (buffSlot.buff.IsBuff)
        {
            _buffs.Remove(buffSlot);
        }
        else
        {
            _debuffs.Remove(buffSlot);
        }
       

        _onChangedControlSlots?.Invoke(_controlSlots);

        
    }

    public void RemoveBuffStats(BuffSlot buffSlot)
    {
        _statsController.RemoveBuffStats(buffSlot.buff.LevelData[buffSlot.level - 1].Stats, buffSlot.stack);
    }



    public void ApplyBuff(BuffSlot buffSlot, Transform transform)
    {
        

        BuffSlot newBuffSlot = new BuffSlot(buffSlot);



        Debug.Log("#buff# ApplyBuff = " + newBuffSlot.buff.Name + " dur " + newBuffSlot.duration);

        if (newBuffSlot.buff.IsBuff == false)
        {
            _debuffs.Add(newBuffSlot);

            if (newBuffSlot.buff.effectLoop != null) // set effect loop in transform
            {
                GameObject effect = ObjectsPool.Spawn(newBuffSlot.buff.effectLoop, transform.position, Quaternion.identity);
                effect.transform.parent = transform;
                 
                if (effect != null)
                {
                    newBuffSlot.effect = effect;
                }
            }

            if (newBuffSlot.buff.effectOnStart != null) // set effect loop in transform
            {
                GameObject effect = ObjectsPool.Spawn(newBuffSlot.buff.effectOnStart, transform.position, Quaternion.identity);  
            }

            if (newBuffSlot.buff.ControlState != Enums.ControlState.none) // debuff has control state
            {
                StateSlot newControlSlot = new StateSlot(newBuffSlot.buff.ControlState, newBuffSlot);
                _controlSlots.Add(newControlSlot);

                newBuffSlot.ControlSlot = newControlSlot;


                _onChangedControlSlots?.Invoke(_controlSlots);

                if (_controlSlots.Count == 1)
                {

                    // event!!

                    //_enemy.SetControlEffect(newBuffSlot.buff.controlState);
                    //_enemy.GetEnemyAnimatorController().SetControlEffect(newBuffSlot.buff.controlState);
                }
            }

            //stats
            if (newBuffSlot.buff.LevelData[newBuffSlot.level - 1] != null)
            {

                if (newBuffSlot.buff.LevelData[newBuffSlot.level - 1].Stats.Count > 0)
                {
                    newBuffSlot.statSlots = new List<StatSlot>();

                    _statsController.ApplyBuffStats(newBuffSlot.buff.LevelData[newBuffSlot.level - 1].Stats);
                }
            }




        }
        else
        {
            BuffSlot currentBuffSlot = GetBuffSlot(buffSlot);

            if (currentBuffSlot == null) // no buff like this
            {
                newBuffSlot = new BuffSlot(buffSlot.buff, buffSlot.level);
                _buffs.Add(newBuffSlot);
                ApplyBuffStats(newBuffSlot);
            }
            else // trying to stack buff
            {
                if(AddStack(currentBuffSlot))
                {
                    ApplyBuffStats(currentBuffSlot);
                }
                else
                {
                    currentBuffSlot.duration = buffSlot.duration;
                }
            }



            if (newBuffSlot.buff.effectLoop != null) // set effect loop in transform
            {
                GameObject effect = ObjectsPool.Spawn(newBuffSlot.buff.effectLoop, transform.position, Quaternion.identity);
                effect.transform.parent = transform;

                if (effect != null)
                {
                    newBuffSlot.effect = effect;
                }
            }

            if (newBuffSlot.buff.effectOnStart != null) // set effect loop in transform
            {
                GameObject effect = ObjectsPool.Spawn(newBuffSlot.buff.effectOnStart, transform.position, Quaternion.identity);
            }

            if (newBuffSlot.buff.ControlState != Enums.ControlState.none) // debuff has control state
            {
                //StateSlot newControlSlot = new StateSlot(newBuffSlot.buff.ControlState, newBuffSlot);
                //_controlSlots.Add(newControlSlot);

                //newBuffSlot.ControlSlot = newControlSlot;


                //_onChangedControlSlots?.Invoke(_controlSlots);

                //if (_controlSlots.Count == 1)
                //{

                //    // event!!

                //    //_enemy.SetControlEffect(newBuffSlot.buff.controlState);
                //    //_enemy.GetEnemyAnimatorController().SetControlEffect(newBuffSlot.buff.controlState);
                //}
            }

            //stats
            

        }



    }

    private void ApplyBuffStats(BuffSlot currentBuffSlot)
    {
        if (currentBuffSlot.buff.LevelData[currentBuffSlot.level - 1] != null)
        {
            if (currentBuffSlot.buff.LevelData[currentBuffSlot.level - 1].Stats.Count > 0)
            {
                currentBuffSlot.statSlots = new List<StatSlot>();

                _statsController.ApplyBuffStats(currentBuffSlot.buff.LevelData[currentBuffSlot.level - 1].Stats);
            }
        }
    }

    private BuffSlot GetBuffSlot(BuffSlot buffSlot)
    {
        if (buffSlot.buff.IsBuff == true)
        {
            foreach(BuffSlot slot in _buffs)
            {
                if(slot.buff == buffSlot.buff)
                {
                    return slot;
                }
            }
        }
        else
        {
            foreach (BuffSlot slot in _debuffs)
            {
                if (slot.buff == buffSlot.buff)
                {
                    return slot;
                }
            }
        }

        return null;
    }


    private bool AddStack(BuffSlot buffSlot)
    {
        if(buffSlot.buff.LevelData[buffSlot.level - 1].MaxStack > buffSlot.stack)
        {
            buffSlot.stack++;
            
            Debug.Log("#AddStack# " + buffSlot.buff.Name + "  stack  " + buffSlot.stack);

            return true; // stacked successfully
        }

        return false;
    }


}

    [System.Serializable]
public class BuffSlot
{
    public Buff buff;
    public int level;
    public GameObject effect;
    public float duration;
    public List<StatSlot> statSlots;
    public StateSlot ControlSlot;
    public StateSlot PositiveSlot;
    public int amount = 1;
    public int stack = 1;

    public BuffSlot(Buff buff, int level)
    {
        this.buff = buff;
        this.level = level;
        this.duration = buff.LevelData[level-1].Duration;
    }

    public BuffSlot(BuffSlot slot)
    {
        this.buff = slot.buff;
        this.level = slot.level;
        this.duration = slot.duration;
    }
}


public class StateSlot
{
    public Enums.CharacterStates state;
    public Enums.ControlState controlState;
    public BuffSlot buffSlot;
    public int amount = 1;

    public StateSlot(Enums.CharacterStates state, BuffSlot buffSlot, int amount = 1)
    {
        this.state = state;
        this.buffSlot = buffSlot;
        this.amount = amount;
    }

    public StateSlot(Enums.ControlState controlState, BuffSlot buffSlot, int amount = 1)
    {
        this.controlState = controlState;
        this.buffSlot = buffSlot;
        this.amount = amount;
    }
}




[System.Serializable]
public class BuffDataRPCSlot  // for rpc
{
    public int Id;
    public float Duration;
    public int Level;

    public string[] Targets;

    public BuffDataRPCSlot(BuffSlot buffSlot, List<ITarget> list)
    {
        Id = buffSlot.buff.Id;
        Duration = buffSlot.duration;
        Level = buffSlot.level;

        Targets = new string[list.Count];

        for(int i = 0; i < list.Count; i++)
        {
            Targets[i] = list[i].GetId();
        }
    }

    public BuffDataRPCSlot(BuffSlot buffSlot, ITarget target)
    {
        Id = buffSlot.buff.Id;
        Duration = buffSlot.duration;
        Level = buffSlot.level;

        Targets = new string[1];

         
        Targets[0] = target.GetId(); 
    }
}

[System.Serializable]
public class HealDataRPCSlot  // for rpc
{
    public string UserId;  

    public float Value;

    public string[] Targets;

    public HealDataRPCSlot(string id, float value, string[] targets)
    {
        UserId = id;
        Value = value;
        Targets = targets;
    }
}