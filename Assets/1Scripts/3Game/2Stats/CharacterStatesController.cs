using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatesController 
{
    
    public bool CanMove { get;  private set; }
    public bool CanCast { get; private set; }
    public bool CanAttack { get; private set; }
    public bool CanRotate { get; private set; }



    private Action<Enums.ControlState, bool, bool, bool, bool> _controlStatesCallback;


    public CharacterStatesController(Action<Enums.ControlState, bool, bool, bool, bool> controlStatesCallback)
    {


        _controlStatesCallback = controlStatesCallback;

    }





    public void OnControlStatesChanged(List<StateSlot> list)
    {
        if(list.Count == 0)
        {
            SetNormalState();
            _controlStatesCallback?.Invoke(Enums.ControlState.none, CanMove, CanCast, CanAttack, CanRotate);
            return;
        }

        Enums.ControlState controlStateToshow = GetControlStateToShow(list);



        foreach (StateSlot slot in list)
        {
            CheckStates(slot.controlState);
        }



        _controlStatesCallback?.Invoke(controlStateToshow, CanMove, CanCast, CanAttack, CanRotate);
    }

    private Enums.ControlState GetControlStateToShow(List<StateSlot> list)
    {
        if(IsControlState(Enums.ControlState.Frozen, list))
        {
            return Enums.ControlState.Frozen;
        }
        else if (IsControlState(Enums.ControlState.Stun, list))
        {
            return Enums.ControlState.Stun;
        }


        return Enums.ControlState.none;
    }

    private bool IsControlState(Enums.ControlState state, List<StateSlot> list)
    {
        foreach (StateSlot slot in list)
        {
            if(state == slot.controlState)
            {
                return true;
            }
        }

        return false;
    }

    private void CheckStates(Enums.ControlState state)
    {
        if(state == Enums.ControlState.Stun)
        {
            CanMove = false;
            CanCast = false;
            CanAttack = false;
            CanRotate = false;
        }
    }




    private void SetNormalState()
    {
        CanMove = true;
        CanCast = true;
        CanAttack = true;
        CanRotate = true;
    }
}
