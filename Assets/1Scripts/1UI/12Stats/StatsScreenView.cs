using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsScreenView : ScreenView
{
    public UniversalMenuButton[] botButtons;
    public Button[] statsTypeButtons;

    public UniversalParameter[] statsParametersSlots;
    public Button closeInPlaymodeButton;


    //portrait part
    public Image portrait;
    public TMPro.TMP_Text className;
    public TMPro.TMP_Text level;

    public override ScreenController Construct()
    {
        return new StatsScreenController(this);
    }

}
