using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsScreenView : ScreenView
{
    public Button continueButton;
    public StatisticsSlotUI[] slots;



    public override ScreenController Construct()
    {
        return new StatisticsScreenController(this);
    }

}
