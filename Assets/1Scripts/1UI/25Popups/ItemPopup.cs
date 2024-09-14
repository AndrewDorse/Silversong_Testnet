using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silversong.UI
{

    public class ItemPopup : Popup
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;

        [SerializeField] private TMPro.TMP_Text _itemNameText;
        [SerializeField] private TMPro.TMP_Text _itemRarityText;
        [SerializeField] private TMPro.TMP_Text _itemDescriptionText;

        [SerializeField] private UniversalButton _mainButton;
        [SerializeField] private UniversalButton _secondaryButton;



        public void Setup(InventoryItem item, Enums.UniversalButtonType mainButtonType, Action mainAction, int value)
        {
            _icon.sprite = item.Icon;
            _itemNameText.text = item.Name;



            _mainButton.Setup(mainButtonType, ( () => 
            {
                mainAction?.Invoke();
                _backBGButton.onClick.Invoke();
            }), value
            );
        }


    }


}