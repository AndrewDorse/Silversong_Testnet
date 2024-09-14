using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/ActiveAbility")]

public class ActiveAbility : BaseItem
{
    [Tooltip ("Base Info")]
    public int MaxLevel = 5;

    public Enums.MagicSchools MagicSchool = Enums.MagicSchools.None;
    public Enums.AbilityTarget Target = Enums.AbilityTarget.Enemies;
    public Enums.AbilityActionType ActionType = Enums.AbilityActionType.damage;
    public Enums.AbilityCostType CostType = Enums.AbilityCostType.mana;
    public Enums.AbilityCastTypes CastType = Enums.AbilityCastTypes.withoutTarget;
    public Enums.AbilityConcreteTypes AbilityConcreteType = Enums.AbilityConcreteTypes.attackSkill;

    public Enums.Animations CastAnimation = Enums.Animations.CastTaunt;

    public Buff Buff;



    [Tooltip("Prefabs")]
    public UnityEngine.AddressableAssets.AssetReferenceGameObject EffectPrefabAddress;
    public UnityEngine.AddressableAssets.AssetReferenceGameObject EffectTargetPrefabAddress;
    public UnityEngine.AddressableAssets.AssetReferenceGameObject ProjectilePrefabAddress;


    [Tooltip("Per Level Info")]
    public ActiveAbilityLevelData[] LevelInfo;


    // require to learn

}

[System.Serializable]
public class ActiveAbilityLevelData
{
    public int RequiredPoints = 1;

    public float Cooldown = 3;
    public float CastTime = 0;
    public float Cost = 15;
    public float BaseValue = 0;

    public float Radius = 0;
    public float FrontOffset = 0;


    public List<StatSlot> StatSlots;
}