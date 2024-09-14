using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController
{
    public float Speed;

    private Animator _animator;

    private Enums.AnimatorStates _state = Enums.AnimatorStates.normal;


    private Enums.Animations _currentAnimation;


    public bool NeedToUseMeleeAttack = false;
    private bool _needToUseRangeAttack = false;


    public EnemyAnimatorController(Animator animator)
    {
        Setup(animator);
    }

    public void Setup(Animator animator)
    {
        _animator = animator;
        EventsProvider.TenTimesPerSecond += CheckAnimatorState;
    }

    public void SetControlState(Enums.ControlState controlState)
    {
        if(controlState == Enums.ControlState.none)
        {
            _state = Enums.AnimatorStates.normal;

            return;
        }
        
        if(controlState == Enums.ControlState.Stun)
        {
            _state = Enums.AnimatorStates.underControl;
            SetAnimation(Enums.Animations.Stun);
        }



    }







    private void CheckAnimatorState()
    {
        if (_state == Enums.AnimatorStates.normal)
        {
            if (NeedToUseMeleeAttack)
            {
                SetAnimation(Enums.Animations.Fight);
            }
            else if (_needToUseRangeAttack)
            {
                SetAnimation(Enums.Animations.FightRange);
            }
            else
            {
                if (Speed < 0.25f)
                {
                    SetAnimation(Enums.Animations.Idle);
                }
                else
                {
                    SetAnimation(Enums.Animations.Run);
                }
            }
        }
    }


    private void SetAnimation(Enums.Animations newAnimation)
    {
        if (newAnimation == _currentAnimation)
        {
            return;
        }

        TurnOffAnimation((int)_currentAnimation);

        int number = (int)newAnimation;

        TurnOnAnimation(number);

        _currentAnimation = newAnimation;
        // weights ???
    }


    private void TurnOnAnimation(int number)
    {
        if (number < 10)  // base states
        {
            _animator.SetBool(((Enums.Animations)number).ToString(), true);

        }
        else if (number < 50)  // triggers states
        {
            _animator.SetTrigger(((Enums.Animations)number).ToString());
        }
        else if (number >= 50)  // control states
        {
            _animator.SetBool(((Enums.Animations)number).ToString(), true);
            _animator.SetLayerWeight(1, 1);
        }
    }


    private void TurnOffAnimation(int animationId)
    {
        if (animationId < 10)  // base states
        {
            _animator.SetBool(((Enums.Animations)animationId).ToString(), false);
        }
        else if (animationId < 50)  // triggers states
        {
            _animator.ResetTrigger(((Enums.Animations)animationId).ToString());
        }
        else if (animationId >= 50)  // control states
        {
            _animator.SetBool(((Enums.Animations)animationId).ToString(), false);
            _animator.SetLayerWeight(1, 0);
        }
    }










    public void Dispose()
    {
        EventsProvider.TenTimesPerSecond -= CheckAnimatorState;
    }
}