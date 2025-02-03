using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class FightCalculation
{
    
    public static void CalculateDamage(StatsController attacking, StatsController attacked, string attackingId)
    {

        // Calculate exact damage defend

        float damage = attacking.GetStat(Enums.Stats.meleeDamage);
        float armor = attacked.GetStat(Enums.Stats.armor);
        float damageReduction = attacked.GetStat(Enums.Stats.defensePc);


        damage *= 1 - damageReduction;

        attacked.ReduceHealth(attackingId, damage);

        Debug.Log("#Damage Melee #" + attackingId + "  " + damage);
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
            {
                BuffSlot buffSlot = null;

                if (abilitySlot.activeAbility.Buff)
                {
                    buffSlot = new BuffSlot(abilitySlot.activeAbility.Buff, abilitySlot.level);
                    caster.GetStatsController().ApplyBuff(buffSlot);
                }





            }
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
            else if (abilitySlot.activeAbility.Target == Enums.AbilityTarget.closestAnotherAlly)
            {
                ITarget target = TargetProvider.GetClosestAlly(caster.GetPosition(), caster).Item1;

                if (target != null)
                {
                    if (abilitySlot.activeAbility.ActionType == Enums.AbilityActionType.heal)
                    {
                        float value = CalculateValue(caster, abilitySlot);

                        if(target.IsAi() == true) // should work only on Master
                        {
                            target.GetStatsController().Heal(value);
                        }
                        else
                        {
                            GameRPCController.instance.SendHealDataToOthers(JsonUtility.ToJson(
                                new HealDataRPCSlot(
                                    caster.GetId(),
                                    value,
                                    new string[] { target.GetId() } 
                                    )));
                        }                        
                    }
                    else if (abilitySlot.activeAbility.ActionType == Enums.AbilityActionType.buff)
                    {
                        BuffSlot buffSlot = null;

                        if (abilitySlot.activeAbility.Buff)
                        {
                            buffSlot = new BuffSlot(abilitySlot.activeAbility.Buff, abilitySlot.level); 
                        } 

                        if (target.IsAi() == true) // should work only on Master
                        {
                            target.GetStatsController().ApplyBuff(buffSlot);
                        }
                        else
                        {
                            GameRPCController.instance.SendBuffApplyDataToOthers(JsonUtility.ToJson(new BuffDataRPCSlot(buffSlot, target)));
                        }
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






    private static float CalculateValue(ITarget caster, ActiveAbilitySlot abilitySlot)
    {
        ActiveAbilityLevelData levelData = abilitySlot.activeAbility.LevelInfo[abilitySlot.level - 1];

        float result = levelData.BaseValue;
         
        foreach(var slot in levelData.StatSlots)
        {
            float currentValue = caster.GetStatsController().GetStat(slot.stat);

            result += currentValue * slot.value;
        } 

        return result;
    }







}