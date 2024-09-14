using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silversong.UI
{
    public class UniversalButton : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _buttonText;
        [SerializeField] private TMPro.TextMeshProUGUI _valueText;
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;


        [SerializeField] private Sprite[] _sprites;


        public void Setup(Enums.UniversalButtonType openType, Action callback, int value = 1)
        {

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => callback?.Invoke());

            //nft
            if (openType == Enums.UniversalButtonType.available)
            {
                _button.interactable = true;
                _buttonText.text = "Choose";

                _icon.gameObject.SetActive(false);
                _valueText.gameObject.SetActive(false);
            }
            else if (openType == Enums.UniversalButtonType.unavailable)
            {
                _button.interactable = false;
                _buttonText.text = "Unavailable";

                _icon.gameObject.SetActive(false);
                _valueText.gameObject.SetActive(false);
            }
            else if (openType == Enums.UniversalButtonType.choosen)
            {
                _button.interactable = false;
                _buttonText.text = "Choosen";

                _icon.gameObject.SetActive(false);
                _valueText.gameObject.SetActive(false);
            }


            // items
            else if (openType == Enums.UniversalButtonType.canBuy)
            {
                _button.interactable = true;
                _buttonText.text = "Buy";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _valueText.text = value.ToString();

                _icon.sprite = _sprites[0];
            }
            else if (openType == Enums.UniversalButtonType.canSell)
            {
                _button.interactable = true;
                _buttonText.text = "Sell";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _valueText.text = Mathf.RoundToInt(value).ToString();
            }
            else if (openType == Enums.UniversalButtonType.notEnoughMoney)
            {
                _button.interactable = false;
                _buttonText.text = "Not Enough Money";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _icon.sprite = _sprites[0];
                _valueText.text = value.ToString();
            }
            else if (openType == Enums.UniversalButtonType.noPlace)
            {
                _button.interactable = false;
                _buttonText.text = "No Space";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _icon.sprite = _sprites[0];
                _valueText.text = value.ToString();
            }


            // abilities
            else if (openType == Enums.UniversalButtonType.canLearn)
            {
                _button.interactable = true;
                _buttonText.text = "Learn";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _valueText.text = value.ToString();
                _icon.sprite = _sprites[1];
            }
            else if (openType == Enums.UniversalButtonType.CanUpgrade)
            {
                _button.interactable = true;
                _buttonText.text = "Upgrade";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _valueText.text = value.ToString();
                _icon.sprite = _sprites[1];

            }
            else if (openType == Enums.UniversalButtonType.maxAmount)
            {
                _button.interactable = false;
                _buttonText.text = "Max Amount Of Active Abilities";

                _icon.gameObject.SetActive(false);
                _valueText.gameObject.SetActive(false);
            }
            else if (openType == Enums.UniversalButtonType.notEnoughPointsToLearn)
            {
                _button.interactable = false;
                _buttonText.text = "Not Enough Points To Learn";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _icon.sprite = _sprites[1];
            }
            else if (openType == Enums.UniversalButtonType.notEnoughPointsToUpgrade)
            {
                _button.interactable = false;
                _buttonText.text = "Not Enough Points To Upgrade";

                _icon.gameObject.SetActive(true);
                _valueText.gameObject.SetActive(true);
                _icon.sprite = _sprites[1];
            }
            else if (openType == Enums.UniversalButtonType.maxLevel)
            {
                _button.interactable = false;
                _buttonText.text = "The Ability Is Max Level";

                _icon.gameObject.SetActive(false);
                _valueText.gameObject.SetActive(false);
            }







            else if (openType == Enums.UniversalButtonType.backButton)
            {
                _button.interactable = true;
                _buttonText.text = "Back";

                _icon.gameObject.SetActive(false);
                _valueText.gameObject.SetActive(false);
            }
        }

    }

}