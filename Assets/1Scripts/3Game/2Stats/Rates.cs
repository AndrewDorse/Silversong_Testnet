using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rates
{
    public static float NewDefenseRate(int level) // for physical damage 
    {
        float rate = 80 / (((level - 1) * 9f) + 100f);
        return rate;
    }

    public static float NewAttackSpeedRate(int level) // for attack speed
    {
        float rate = 2 / (((level - 1) * 9f) + 100f);
        return rate;
    }

    public static float NewStatsRate(int level) // for all stats evade block parry crit etc
    {
        float rate = 66 / (((level - 1) * 9f) + 100f);
        return rate;
    }

    public static float DamageRate(int level) // for all stats evade block parry crit etc
    {
        float multiplie = (float)Math.Pow(1.15f, level);
        return multiplie;
    }
}
