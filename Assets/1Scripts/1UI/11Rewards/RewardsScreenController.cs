using Silversong.Game;
using Silversong.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsScreenController : ScreenController
{
    private readonly RewardsScreenView _view;

    public RewardsScreenController(RewardsScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        SetRewardsView(GameMaster.RewardsController.RewardSlots);


        _view.continueButton.onClick.AddListener(ContinueButton);

    }



    private void SetRewardsView(RewardSlot[] rewards)
    {
        for (int i = 0; i < _view.rewardSlotUIs.Length; i++)
        {
            if (i < rewards.Length)
            {
                _view.rewardSlotUIs[i].Setup(rewards[i], Click);
            }
            else
            {
                _view.rewardSlotUIs[i].gameObject.SetActive(false);
            }



        }
    }

    private void Click(RewardSlot rewardSlot)
    {

        if (rewardSlot.RewardType == Enums.RewardType.Item)
        {
            ItemPopup itemPopup = Master.instance.GetPopup(Enums.PopupType.item) as ItemPopup;

            InventoryItem item = InfoProvider.instance.GetItem(rewardSlot.Value);


            itemPopup.Setup(item, Enums.UniversalButtonType.backButton, null, 0);
        }


    }

    private void ContinueButton()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.camp);
    }

}
