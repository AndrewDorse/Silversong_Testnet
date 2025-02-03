using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHolder
{
    public float[] stats = new float[501]; // summ / base / passive / item / buff + debuff
    public Enums.MainAttributes mainAttribute;
    public float[] levelRates = new float[3];

    public StatsHolder(Enums.MainAttributes mainAttribute = Enums.MainAttributes.strength)
    {
        this.mainAttribute = mainAttribute;
        SetStartValues();
        UpdateRates(1);

        CalculateStats();
    }

    public void UpdateRates(int level)
    {
        levelRates[0] = Rates.NewAttackSpeedRate(level); //as
        levelRates[1] = Rates.NewDefenseRate(level); //defense
        levelRates[2] = Rates.NewStatsRate(level); //block evade crit etc
    }


    public void SetStartValues()
    {
        stats[(int)Enums.Stats.hp + 100] = 100;
        stats[(int)Enums.Stats.hpPc + 100] = 1;
        stats[(int)Enums.Stats.mp + 100] = 100;
        stats[(int)Enums.Stats.mpPc + 100] = 1;

        stats[(int)Enums.Stats.strength + 100] = 0;
        stats[(int)Enums.Stats.strengthPc + 100] = 1;
        stats[(int)Enums.Stats.agility + 100] = 0;
        stats[(int)Enums.Stats.agilityPc + 100] = 1;
        stats[(int)Enums.Stats.intelligence + 100] = 0;
        stats[(int)Enums.Stats.intelligencePc + 100] = 1;
        stats[(int)Enums.Stats.stamina + 100] = 0;
        stats[(int)Enums.Stats.staminaPc + 100] = 1;
        stats[(int)Enums.Stats.spirit + 100] = 0;
        stats[(int)Enums.Stats.spiritPc + 100] = 1;
        stats[(int)Enums.Stats.charisma + 100] = 0;
        stats[(int)Enums.Stats.charismaPc + 100] = 1;
        stats[(int)Enums.Stats.luck + 100] = 0;
        stats[(int)Enums.Stats.luckPc + 100] = 1;

        stats[(int)Enums.Stats.coeffAllDamage + 100] = 1;
        stats[(int)Enums.Stats.coeffAllArmor + 100] = 1;
        stats[(int)Enums.Stats.coeffAllAttackSpeed + 100] = 1;
        stats[(int)Enums.Stats.coeffAllIncomingDamage + 100] = 1;
        stats[(int)Enums.Stats.coeffAllIncomingHeal + 100] = 1;
        stats[(int)Enums.Stats.coeffAllMovementSpeed + 100] = 1;
        stats[(int)Enums.Stats.coeffAllSpellSpeed + 100] = 1;

        stats[(int)Enums.Stats.meleeAttack + 100] = 10;
        stats[(int)Enums.Stats.meleeAttackPc + 100] = 1;
        stats[(int)Enums.Stats.rangeAttack + 100] = 5;
        stats[(int)Enums.Stats.rangeAttackPc + 100] = 1;
        stats[(int)Enums.Stats.armor + 100] = 1;
        stats[(int)Enums.Stats.armorPc + 100] = 1;

        stats[(int)Enums.Stats.spellPowerPc + 100] = 1;
        stats[(int)Enums.Stats.spellDamagePc + 100] = 1;
        stats[(int)Enums.Stats.spellHealPc + 100] = 1;


        stats[(int)Enums.Stats.movementSpeed + 100] = 250;

    }



    private float SummByStat(Enums.Stats stat)
    {
        return stats[(int)stat + 100] + stats[(int)stat + 200] +
            stats[(int)stat + 300] + stats[(int)stat + 400];
    }

    public void CalculateStats()
    {
        /////////////////////     COEFFS    ////////////////////////////
        stats[(int)Enums.Stats.coeffAllAttackSpeed] = SummByStat(Enums.Stats.coeffAllAttackSpeed);
        stats[(int)Enums.Stats.coeffAllArmor] = SummByStat(Enums.Stats.coeffAllArmor);
        stats[(int)Enums.Stats.coeffAllDamage] = SummByStat(Enums.Stats.coeffAllDamage);
        stats[(int)Enums.Stats.coeffAllIncomingDamage] = SummByStat(Enums.Stats.coeffAllIncomingDamage);
        stats[(int)Enums.Stats.coeffAllIncomingHeal] = SummByStat(Enums.Stats.coeffAllIncomingHeal);
        stats[(int)Enums.Stats.coeffAllMovementSpeed] = SummByStat(Enums.Stats.coeffAllMovementSpeed);
        stats[(int)Enums.Stats.coeffAllSpellSpeed] = SummByStat(Enums.Stats.coeffAllSpellSpeed);

        ////////////////////      BASIC   //////////////////////////
        stats[(int)Enums.Stats.strengthPc] = SummByStat(Enums.Stats.strengthPc);
        stats[(int)Enums.Stats.strength] = Mathf.Ceil(SummByStat(Enums.Stats.strength) * stats[(int)Enums.Stats.strengthPc]);
        stats[(int)Enums.Stats.agilityPc] = SummByStat(Enums.Stats.agilityPc);
        stats[(int)Enums.Stats.agility] = Mathf.Ceil(SummByStat(Enums.Stats.agility) * stats[(int)Enums.Stats.agilityPc]);
        stats[(int)Enums.Stats.intelligencePc] = SummByStat(Enums.Stats.intelligencePc);
        stats[(int)Enums.Stats.intelligence] = Mathf.Ceil(SummByStat(Enums.Stats.intelligence) * stats[(int)Enums.Stats.intelligencePc]);
        stats[(int)Enums.Stats.staminaPc] = SummByStat(Enums.Stats.staminaPc);
        stats[(int)Enums.Stats.stamina] = Mathf.Ceil(SummByStat(Enums.Stats.stamina) * stats[(int)Enums.Stats.staminaPc]);
        stats[(int)Enums.Stats.spiritPc] = SummByStat(Enums.Stats.spiritPc);
        stats[(int)Enums.Stats.spirit] = Mathf.Ceil(SummByStat(Enums.Stats.spirit) * stats[(int)Enums.Stats.spiritPc]);
        stats[(int)Enums.Stats.charismaPc] = SummByStat(Enums.Stats.charismaPc);
        stats[(int)Enums.Stats.charisma] = Mathf.Ceil(SummByStat(Enums.Stats.charisma) * stats[(int)Enums.Stats.charismaPc]);
        stats[(int)Enums.Stats.luckPc] = SummByStat(Enums.Stats.luckPc);
        stats[(int)Enums.Stats.luck] = Mathf.Ceil(SummByStat(Enums.Stats.luck) * stats[(int)Enums.Stats.luckPc]);

        ////////////////////    ATTACK    ////////////////////////////
        // melee attack
        stats[(int)Enums.Stats.meleeAttack] = SummByStat(Enums.Stats.meleeAttack);
        stats[(int)Enums.Stats.meleeAttackPc] = SummByStat(Enums.Stats.meleeAttackPc);
        // range attack 
        stats[(int)Enums.Stats.rangeAttack] = SummByStat(Enums.Stats.rangeAttack);
        stats[(int)Enums.Stats.rangeAttackPc] = SummByStat(Enums.Stats.rangeAttackPc);
        // as rate
        stats[(int)Enums.Stats.attackSpeedRate] = SummByStat(Enums.Stats.attackSpeedRate) + (stats[(int)Enums.Stats.agility] / 5);
        stats[(int)Enums.Stats.attackSpeedFinal] = (((stats[(int)Enums.Stats.attackSpeedRate] * levelRates[0])) + 1) * stats[(int)Enums.Stats.coeffAllAttackSpeed];
        stats[(int)Enums.Stats.attackSpeedFinal] = (float)Math.Round(stats[(int)Enums.Stats.attackSpeedFinal], 2);
        if (stats[(int)Enums.Stats.attackSpeedFinal] > 3f) stats[(int)Enums.Stats.attackSpeedFinal] = 3;
        // mastery rate
        stats[(int)Enums.Stats.masteryRate] = SummByStat(Enums.Stats.masteryRate) + ((stats[(int)Enums.Stats.strength] + stats[(int)Enums.Stats.agility]) / 10);
        stats[(int)Enums.Stats.masteryFinal] = stats[(int)Enums.Stats.masteryRate];
        // crit chance
        stats[(int)Enums.Stats.critChanceRate] = Mathf.CeilToInt(SummByStat(Enums.Stats.critChanceRate) + (stats[(int)Enums.Stats.agility] / 5));
        stats[(int)Enums.Stats.critChanceFinalPc] = stats[(int)Enums.Stats.critChanceRate] * levelRates[2] / 100;
        stats[(int)Enums.Stats.critChanceFinalPc] = (float)Math.Round(stats[(int)Enums.Stats.critChanceFinalPc], 2);
        // crit damage
        stats[(int)Enums.Stats.critDamageRate] = SummByStat(Enums.Stats.critDamageRate) + (stats[(int)Enums.Stats.strength] / 5);
        stats[(int)Enums.Stats.critDamageFinalPc] = 1.5f + (stats[(int)Enums.Stats.critDamageRate] * levelRates[2] / 100);
        stats[(int)Enums.Stats.critDamageFinalPc] = (float)Math.Round(stats[(int)Enums.Stats.critDamageFinalPc], 2);
        // arp
        stats[(int)Enums.Stats.armorPenetrationRate] = SummByStat(Enums.Stats.armorPenetrationRate) + (stats[(int)Enums.Stats.strength] / 5);
        stats[(int)Enums.Stats.armorPenetrationFinal] = stats[(int)Enums.Stats.armorPenetrationRate];

        /////////////////////    DEFENSE      ////////////////////////////
        // armor
        stats[(int)Enums.Stats.armorPc] = SummByStat(Enums.Stats.armorPc);
        stats[(int)Enums.Stats.armor] = SummByStat(Enums.Stats.armor) * stats[(int)Enums.Stats.armorPc] * stats[(int)Enums.Stats.coeffAllArmor];
        stats[(int)Enums.Stats.armor] = Mathf.CeilToInt(stats[(int)Enums.Stats.armor]);
        // parry
        stats[(int)Enums.Stats.parryRate] = SummByStat(Enums.Stats.parryRate) + ((stats[(int)Enums.Stats.strength] + stats[(int)Enums.Stats.agility]) / 10);
        stats[(int)Enums.Stats.parryFinalPc] = stats[(int)Enums.Stats.parryRate] * levelRates[2] / 100;
        if (stats[(int)Enums.Stats.parryFinalPc] > 0.66f) stats[(int)Enums.Stats.parryFinalPc] = 0.66f;
        // block
        stats[(int)Enums.Stats.blockRate] = SummByStat(Enums.Stats.blockRate) + (stats[(int)Enums.Stats.stamina] / 5);
        stats[(int)Enums.Stats.blockFinalPc] = stats[(int)Enums.Stats.blockRate] * levelRates[2] / 100;// TODO add multiplie
        if (stats[(int)Enums.Stats.blockFinalPc] > 0.66f) stats[(int)Enums.Stats.blockFinalPc] = 0.66f;
        // evade
        stats[(int)Enums.Stats.evadeRate] = SummByStat(Enums.Stats.evadeRate) + (stats[(int)Enums.Stats.agility] / 5);
        stats[(int)Enums.Stats.evadeFinalPc] = stats[(int)Enums.Stats.evadeRate] * levelRates[2] / 100;
        if (stats[(int)Enums.Stats.evadeFinalPc] > 0.66f) stats[(int)Enums.Stats.evadeFinalPc] = 0.66f;
        // hp reg
        stats[(int)Enums.Stats.hpRegeneration] = SummByStat(Enums.Stats.hpRegeneration);
        // defense
        stats[(int)Enums.Stats.defensePc] = (stats[(int)Enums.Stats.armor] * levelRates[1]) / 100;
        stats[(int)Enums.Stats.defensePc] = (float)Math.Round(stats[(int)Enums.Stats.defensePc], 2);
        if (stats[(int)Enums.Stats.defensePc] > 0.8f) stats[(int)Enums.Stats.defensePc] = 0.8f;

        ////////////////////      HP / MP     /////////////////////////////
        // hp
        stats[(int)Enums.Stats.hpPc] = SummByStat(Enums.Stats.hpPc);
        stats[(int)Enums.Stats.hp] = Mathf.CeilToInt(((SummByStat(Enums.Stats.hp) + (stats[(int)Enums.Stats.strength] * 2) + (stats[(int)Enums.Stats.stamina] * 8))) * stats[(int)Enums.Stats.hpPc]);
        // mp
        stats[(int)Enums.Stats.mpPc] = SummByStat(Enums.Stats.mpPc);
        stats[(int)Enums.Stats.mp] = Mathf.CeilToInt(((SummByStat(Enums.Stats.mp) + (stats[(int)Enums.Stats.intelligence] * 3) + (stats[(int)Enums.Stats.spirit] * 7))) * stats[(int)Enums.Stats.mpPc]);

        ////////////////////       MAGIC        /////////////////////////////
        // spell power
        stats[(int)Enums.Stats.spellPowerPc] = SummByStat(Enums.Stats.spellPowerPc);
        stats[(int)Enums.Stats.spellPower] = (SummByStat(Enums.Stats.spellPower) + (stats[(int)Enums.Stats.intelligence] / 2)) * stats[(int)Enums.Stats.spellPowerPc];
        // spell damage
        stats[(int)Enums.Stats.spellDamagePc] = SummByStat(Enums.Stats.spellDamagePc);
        stats[(int)Enums.Stats.spellDamage] = (stats[(int)Enums.Stats.spellPower] + SummByStat(Enums.Stats.spellDamage)) * stats[(int)Enums.Stats.spellDamagePc];
        // spell heal
        stats[(int)Enums.Stats.spellHealPc] = SummByStat(Enums.Stats.spellHealPc);
        stats[(int)Enums.Stats.spellHeal] = (stats[(int)Enums.Stats.spellPower] + SummByStat(Enums.Stats.spellHeal)) * stats[(int)Enums.Stats.spellHealPc];
        //// spell crit chance
        stats[(int)Enums.Stats.spellCritChanceRate] = SummByStat(Enums.Stats.spellCritChanceRate) + (stats[(int)Enums.Stats.charisma] / 5);
        stats[(int)Enums.Stats.spellCritChanceFinalPc] = stats[(int)Enums.Stats.spellCritChanceRate] * levelRates[2] / 100;
        stats[(int)Enums.Stats.spellCritChanceFinalPc] = (float)Math.Round(stats[(int)Enums.Stats.spellCritChanceFinalPc], 2);
        //// spell crit damage
        stats[(int)Enums.Stats.spellCritDamageRate] = SummByStat(Enums.Stats.spellCritDamageRate) + (stats[(int)Enums.Stats.intelligence] / 5);
        stats[(int)Enums.Stats.spellCritDamageFinalPc] = 1.25f + (stats[(int)Enums.Stats.spellCritDamageRate] * levelRates[2] / 100);
        stats[(int)Enums.Stats.spellCritDamageFinalPc] = (float)Math.Round(stats[(int)Enums.Stats.spellCritDamageFinalPc], 2);
        //// spell speed
        stats[(int)Enums.Stats.spellSpeedRate] = SummByStat(Enums.Stats.spellSpeedRate);
        stats[(int)Enums.Stats.spellSpeedFinal] = (SummByStat(Enums.Stats.spellSpeedRate) * levelRates[2]) / 100;
        stats[(int)Enums.Stats.spellSpeedFinal] = (float)Math.Round(stats[(int)Enums.Stats.spellSpeedFinal], 2);
        //// spell penetration
        stats[(int)Enums.Stats.spellPenetrationRate] = SummByStat(Enums.Stats.spellPenetrationRate) + (stats[(int)Enums.Stats.intelligence] / 5);
        stats[(int)Enums.Stats.spellPenetrationFinalPc] = stats[(int)Enums.Stats.spellPenetrationRate];
        // concentration
        stats[(int)Enums.Stats.concentrationRate] = SummByStat(Enums.Stats.concentrationRate);
        // magic defense
        stats[(int)Enums.Stats.magicDefenseRate] = SummByStat(Enums.Stats.magicDefenseRate) + (stats[(int)Enums.Stats.spirit] / 5);
        stats[(int)Enums.Stats.magicDefenseFinalPc] = stats[(int)Enums.Stats.magicDefenseRate] * levelRates[2] / 100;
        if (stats[(int)Enums.Stats.magicDefenseFinalPc] > 0.8f) stats[(int)Enums.Stats.magicDefenseFinalPc] = 0.8f;
        // mp regen
        stats[(int)Enums.Stats.mpRegeneration] = SummByStat(Enums.Stats.mpRegeneration) + stats[(int)Enums.Stats.intelligence] / 25;

        ////////////////////       SPELL POWER (DAMAGE) TYPES COEFFICIENTS      /////////////////////////////
        //// fire
        stats[(int)Enums.Stats.firePowerPc] = SummByStat(Enums.Stats.firePowerPc);
        //// ice
        stats[(int)Enums.Stats.icePowerPc] = SummByStat(Enums.Stats.icePowerPc);
        //// electro
        stats[(int)Enums.Stats.electricPowerPc] = SummByStat(Enums.Stats.electricPowerPc);
        //// nature
        stats[(int)Enums.Stats.arcanePowerPc] = SummByStat(Enums.Stats.arcanePowerPc);
        //// poison damage
        stats[(int)Enums.Stats.poisonPowerPc] = SummByStat(Enums.Stats.poisonPowerPc);
        //// dark arts
        stats[(int)Enums.Stats.darkArtsPowerPc] = SummByStat(Enums.Stats.darkArtsPowerPc);
        //// light
        stats[(int)Enums.Stats.lightPowerPc] = SummByStat(Enums.Stats.lightPowerPc);
        //// bloodshed
        stats[(int)Enums.Stats.bloodShedPowerPc] = SummByStat(Enums.Stats.bloodShedPowerPc);

        ////////////////////       RESISTS        /////////////////////////////
        //// fire
        stats[(int)Enums.Stats.fireResistPc] = SummByStat(Enums.Stats.fireResistPc);
        //// ice
        stats[(int)Enums.Stats.iceResistPc] = SummByStat(Enums.Stats.iceResistPc);
        //// electro
        stats[(int)Enums.Stats.electricResistPc] = SummByStat(Enums.Stats.electricResistPc);
        //// nature
        stats[(int)Enums.Stats.arcaneResistPc] = SummByStat(Enums.Stats.arcaneResistPc);
        //// poison
        stats[(int)Enums.Stats.poisonResistPc] = SummByStat(Enums.Stats.poisonResistPc);
        //// dark arts
        stats[(int)Enums.Stats.darkArtsResistPc] = SummByStat(Enums.Stats.darkArtsResistPc);
        //// light
        stats[(int)Enums.Stats.lightResistPc] = SummByStat(Enums.Stats.lightResistPc);
        //// bloodshed
        stats[(int)Enums.Stats.bloodShedResistPc] = SummByStat(Enums.Stats.bloodShedResistPc);

        ////////////////////       RESILIENCES       /////////////////////////////
        //// physical
        stats[(int)Enums.Stats.physicalResilience] = SummByStat(Enums.Stats.physicalResilience) + (stats[(int)Enums.Stats.strength] / 10) + (stats[(int)Enums.Stats.stamina] / 5);
        //// magic
        stats[(int)Enums.Stats.magicResilience] = SummByStat(Enums.Stats.magicResilience) + (stats[(int)Enums.Stats.intelligence] / 10);
        //// mental
        stats[(int)Enums.Stats.mentalResilience] = SummByStat(Enums.Stats.mentalResilience) + (stats[(int)Enums.Stats.spirit] / 10);



        ////////////////////      OTHER     /////////////////////////////
        //// lifesteal
        stats[(int)Enums.Stats.lifestealPc] = SummByStat(Enums.Stats.lifestealPc);
        //// movement speed
        stats[(int)Enums.Stats.movementSpeed] = SummByStat(Enums.Stats.movementSpeed) * stats[(int)Enums.Stats.coeffAllMovementSpeed];

        ////////////////////       BASE DAMAGE      /////////////////////////////
        if (mainAttribute == Enums.MainAttributes.strength)
        {
            stats[(int)Enums.Stats.meleeDamage] = Mathf.Ceil(((stats[(int)Enums.Stats.meleeAttack] * stats[(int)Enums.Stats.meleeAttackPc]) + stats[(int)Enums.Stats.strength]) * stats[(int)Enums.Stats.coeffAllDamage]);
            stats[(int)Enums.Stats.rangeDamage] = Mathf.Ceil(((stats[(int)Enums.Stats.rangeAttack] * stats[(int)Enums.Stats.rangeAttackPc]) + stats[(int)Enums.Stats.strength]) * stats[(int)Enums.Stats.coeffAllDamage]);

        }
        else if (mainAttribute == Enums.MainAttributes.agility)
        {
            stats[(int)Enums.Stats.meleeDamage] = Mathf.Ceil(((stats[(int)Enums.Stats.meleeAttack] * stats[(int)Enums.Stats.meleeAttackPc]) + stats[(int)Enums.Stats.agility]) * stats[(int)Enums.Stats.coeffAllDamage]);
            stats[(int)Enums.Stats.rangeDamage] = Mathf.Ceil(((stats[(int)Enums.Stats.rangeAttack] * stats[(int)Enums.Stats.rangeAttackPc]) + stats[(int)Enums.Stats.agility]) * stats[(int)Enums.Stats.coeffAllDamage]);
        }
        else if (mainAttribute == Enums.MainAttributes.intelligence)
        {
            stats[(int)Enums.Stats.meleeDamage] = Mathf.Ceil(((stats[(int)Enums.Stats.meleeAttack] * stats[(int)Enums.Stats.meleeAttackPc]) + stats[(int)Enums.Stats.intelligence]) * stats[(int)Enums.Stats.coeffAllDamage]);
            stats[(int)Enums.Stats.rangeDamage] = Mathf.Ceil(((stats[(int)Enums.Stats.rangeAttack] * stats[(int)Enums.Stats.rangeAttackPc]) + stats[(int)Enums.Stats.intelligence]) * stats[(int)Enums.Stats.coeffAllDamage]);
        }

    }
}