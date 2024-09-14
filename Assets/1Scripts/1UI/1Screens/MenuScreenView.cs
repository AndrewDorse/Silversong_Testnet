using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreenView : ScreenView
{
    public Button quickStartButton;

    public override ScreenController Construct()
    {
        return new MenuScreenController(this);
    }

}
