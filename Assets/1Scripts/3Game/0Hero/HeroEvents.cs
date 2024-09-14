using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEvents : MonoBehaviour
{
    public void SpellCast()
    {
        EventsProvider.OnAbilityUseTrigger?.Invoke();
    }

    public void Fire()
    {

    }

    public void Weapon_ON()
    {

    }

    public void Weapon_OFF()
    {

    }

    public void AttackAnimation()
    {
        EventsProvider.OnLocalHeroPassiveTrigger?.Invoke(Enums.PassiveTrigger.attackAnimation);
    }
}