using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIconView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;



    public void Setup(InventoryItem item)
    {
        _icon.sprite = item.Icon;
    }

}
