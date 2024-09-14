using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FightCalculation
{
    
    public static void CalculateDamage(StatsController attacking, StatsController attacked, string attackingId)
    {

        // Calculate exact damage defend

        attacked.ReduceHealth(attackingId, attacking.GetStat(Enums.Stats.meleeDamage));

        Debug.Log("#Damage Melee #" + attacking.GetStat(Enums.Stats.meleeDamage));
    }



    public static void CalculateSpell(ITarget caster, ActiveAbilitySlot abilitySlot, Vector3 position = default) // bool useTrigger = true)
    {

        Debug.Log("#018 ApplySpell " + abilitySlot.activeAbility.Name + "  " + position);


        //bool isCritical = false;
        //isCritical = Random.Range(0f, 1f) <= PlayerStateMaster.instance.GetStat(EnumsHandler.Stats.spellCritChanceFinalPc) ? true : false;

        if (abilitySlot.activeAbility.CastType == Enums.AbilityCastTypes.withoutTarget)
        {
            position = caster.GetPosition() + Vector3.up * 1f;
            CreateEffect();

            if (abilitySlot.activeAbility.Target == Enums.AbilityTarget.Caster)
            { }
            else if (abilitySlot.activeAbility.Target == Enums.AbilityTarget.Enemies)
            {
                List<ITarget> list = TargetProvider.GetEnemyTargetsInRadius(caster.GetPosition(),
                         abilitySlot.activeAbility.LevelInfo[abilitySlot.level - 1].Radius);

                BuffSlot buffSlot = null;

                if (abilitySlot.activeAbility.Buff)
                {
                    buffSlot = new BuffSlot(abilitySlot.activeAbility.Buff, abilitySlot.level);
                    GameRPCController.instance.SendBuffApplyDataToOthers(JsonUtility.ToJson(new BuffDataRPCSlot(buffSlot, list)));
                }

                foreach (ITarget target in list)
                {
                    target.GetStatsController().ReduceHealth(caster.GetId(), 1);


                    // check if target resist debuff
                    // apply buff

                    

                    if (abilitySlot.activeAbility.Buff)
                    {
                        target.GetStatsController().ApplyBuff(buffSlot);
                    }
                }

            }


        }



        void CreateEffect()// effect
        {
            if (abilitySlot.activeAbility.EffectPrefabAddress != null)
            {
               // Pool.instance.CreateEffect(spellSlot.spell.effectPrefabAddress, position, GameMaster.instance.GetHeroInstance().transform.rotation, needToSendOthers: true);
            }
        }
    }











}