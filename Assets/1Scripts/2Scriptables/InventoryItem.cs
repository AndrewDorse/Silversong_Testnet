using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/InventoryItem")]

public class InventoryItem : BaseItem
{

    public Enums.Rarity Rarity;
    public int Cost = 10;
    public int Tier = 1;

    public List<StatSlot> Stats;



}
