using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Buff")]

public class Buff : BaseItem
{
    [Tooltip("Base")]

    public bool IsBuff = true;
    public int MaxLevel = 1;
    public Enums.CharacterStates CharacterState = Enums.CharacterStates.none;
    public Enums.ControlState ControlState = Enums.ControlState.none;

    //isDispellible = true, eternal = false;



    [Tooltip("Effect Prefabs")]

    public GameObject effectLoop;
    public GameObject effectOnStart;


    [Tooltip("Data By Level")]
    public List<BuffLevelData> LevelData;
}



[System.Serializable]
public class BuffLevelData
{
    public float Duration = 3;
    public int Amount = 1;
    public int MaxStack = 1;
    public List<StatSlot> Stats;


}
