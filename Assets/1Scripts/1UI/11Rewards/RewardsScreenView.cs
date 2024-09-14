using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsScreenView : ScreenView
{

    public RewardSlotUI[] rewardSlotUIs; 
    public Button continueButton;

    public override ScreenController Construct()
    {
        return new RewardsScreenController(this);
    }

}
