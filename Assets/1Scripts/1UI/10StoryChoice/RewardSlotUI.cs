using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RewardSlotUI : MonoBehaviour
{

    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;


    [SerializeField] private TMPro.TMP_Text _amountText;
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private Button _button;

    public void Setup(RewardSlot rewardSlot, Action<RewardSlot> callback )
    {

        _icon.sprite = DataProvider.RewardsProviderData.GetIconByRewardType(rewardSlot.RewardType, rewardSlot.Value);

        if (_text != null)
        {
            SetText(rewardSlot);
        }

        SetAmountText(rewardSlot);


        _button.onClick.AddListener(() => callback?.Invoke(rewardSlot));

        if (rewardSlot.RewardType == Enums.RewardType.Gold)
        {
            _amountText.text = rewardSlot.Value.ToString();
        }
    }

    private void SetAmountText(RewardSlot rewardSlot)
    {
        if(rewardSlot.RewardType == Enums.RewardType.Gold)
        {
            _amountText.gameObject.SetActive(true);
            _amountText.text = rewardSlot.Value.ToString();
        }
        else
        {
            _amountText.gameObject.SetActive(false);
        }

    }


    private void SetText(RewardSlot rewardSlot)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("For players ");

        if (rewardSlot.RecieverType == Enums.RewardReciever.PlayerWithTag)
        {
            stringBuilder.Append("with tag " + rewardSlot.RequiredTag);
        }
        else if (rewardSlot.RecieverType == Enums.RewardReciever.PlayerWithoutTag)
        {
            stringBuilder.Append("without tag " + rewardSlot.RequiredTag);
        }
        else if (rewardSlot.RecieverType == Enums.RewardReciever.All)
        {
            stringBuilder.Clear();
            stringBuilder.Append("For all players");
        }

        _text.text = stringBuilder.ToString();
    }


}
