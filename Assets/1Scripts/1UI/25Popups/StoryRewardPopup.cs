using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silversong.UI
{

    public class StoryRewardPopup : Popup
    {
        [SerializeField] private Image _icon;

        [SerializeField] private TMPro.TMP_Text _optionText;


        [SerializeField] private RewardSlotUI[] _rewardsSlots;



        public void Setup(StoryOption option)
        {
            _icon.sprite = option.Icon;
            _optionText.text = option.Text;



            SetupRewardSlots(option.RewardSlot);
        }


        private void SetupRewardSlots(RewardSlot[] rewards)
        {
            for (int i = 0; i < _rewardsSlots.Length; i++)
            {
                if (i < rewards.Length)
                {
                    _rewardsSlots[i].Setup(rewards[i], Click);
                }
                else
                {
                    _rewardsSlots[i].gameObject.SetActive(false);
                }



            }
        }

        private void Click(RewardSlot rewardSlot)
        {

            if(rewardSlot.RewardType == Enums.RewardType.Item)
            {
                ItemPopup itemPopup = Master.instance.GetPopup(Enums.PopupType.item) as ItemPopup;

                InventoryItem item = InfoProvider.instance.GetItem(rewardSlot.Value);


                itemPopup.Setup(item, Enums.UniversalButtonType.backButton, null, 0);
            }


        }



    }


}