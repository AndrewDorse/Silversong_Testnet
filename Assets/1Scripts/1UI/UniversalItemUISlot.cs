using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalItemUISlot : MonoBehaviour
{
    [SerializeField] private Image _icon;

    [SerializeField] private Image _frame;


    [SerializeField] private Button _button;

    private int _id;

    [SerializeField] private Sprite _emptySlot;

    public void Setup(InventoryItem item, System.Action<int> callback)
    {
        gameObject.SetActive(true);

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback.Invoke(_id));

        _icon.sprite = item.Icon;

        _id = item.Id;

    }

    public void Setup(BaseItem item, System.Action<int> callback)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback.Invoke(_id));

        _icon.sprite = item.Icon;

        _id = item.Id;

    }

    public void SetEmpty()
    {
        _button.onClick.RemoveAllListeners();
        _icon.sprite = _emptySlot;
    }

}
