using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silversong.UI
{

    public class AbilityPopup : Popup
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _frame;

        [SerializeField] private TMPro.TMP_Text _abilityNameText;
        [SerializeField] private TMPro.TMP_Text _abilityRarityText;
        [SerializeField] private TMPro.TMP_Text _abilityDescriptionText;

        [SerializeField] private UniversalButton _mainButton;
        [SerializeField] private UniversalButton _secondaryButton;



        public void Setup(BaseItem ability, Enums.UniversalButtonType mainButtonType, Action mainAction, int value = 1)
        {
            _icon.sprite = ability.Icon;
            _abilityNameText.text = ability.Name;



            _mainButton.Setup(mainButtonType, ( () => 
            {
                mainAction?.Invoke();
                _backBGButton.onClick.Invoke();
            }),
            value
            );
        }

    }


}