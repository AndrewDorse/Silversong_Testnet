using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/Mob")]
public class Mob : ScriptableObject
{ 
    public int Id;
    public string Name;
    public string Description;
    public List<Enums.Tag> Tags;
    public bool Agressive = true;


    public EnemyModelController Prefab;

    public List<StatSlot> Stats;



    // boss
    // sizes
    // range
    // abilities
}
