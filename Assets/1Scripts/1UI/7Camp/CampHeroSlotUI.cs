using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampHeroSlotUI : MonoBehaviour
{
    [SerializeField] private Image _classIcon;
    [SerializeField] private TMPro.TMP_Text _nicknameText;



    [SerializeField] private ItemIconView[] _itemIcons;


    [SerializeField] private GameObject _readyMark;


    private Action _callback; // ??  ?? 

    public void Setup(PlayerData data, Action callback)
    {
        _classIcon.sprite = InfoProvider.instance.GetHeroClass(data.heroData.classId).icon;
        _nicknameText.text = data.nickname;

        _readyMark.SetActive(data.ready);



        SetupItemIcons(data.heroData.items);
    }


    private void SetupItemIcons(List<ItemSlot> items)
    {
        for(int i = 0; i < _itemIcons.Length; i++)
        {
            if (i < items.Count)
            {
                _itemIcons[i].gameObject.SetActive(true);
                _itemIcons[i].Setup(InfoProvider.instance.GetItem(items[i].Id));
            }
            else
            {
                _itemIcons[i].gameObject.SetActive(false);
            }
        }
    }

}
