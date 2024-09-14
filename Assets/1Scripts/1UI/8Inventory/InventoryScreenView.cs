using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreenView : ScreenView
{
    public UniversalMenuButton[] botButtons;
    public Button[] tierButtons;
    public Button lootButton;

    public UniversalItemUISlot[] shopLootSlots;
    public UniversalItemUISlot[] inventorySlots;


    public override ScreenController Construct()
    {
        return new InventoryScreenController(this);
    }

}
