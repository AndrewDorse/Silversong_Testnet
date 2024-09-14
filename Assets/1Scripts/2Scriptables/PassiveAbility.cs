using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/PassiveAbility")]

public class PassiveAbility : BaseItem
{

    public int MaxLevel = 1;

    public Enums.PassiveTypes Type;
    public Enums.PassiveTrigger Trigger;
    public Enums.MagicSchools MagicSchool;


    public Buff Buff;

    public List<PassiveLevelData> LevelInfo;



    // stats 
    // passives



}

[System.Serializable]
public class PassiveLevelData
{
    public int RequiredPoints;
    public List<StatSlot> Stats;
    public float BaseValue = 1;
    public float Chance = 1;
    public float Cooldown = 1;


}
