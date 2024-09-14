using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampScreenView : ScreenView
{
    public UniversalMenuButton[] botButtons;

    public Button readyButton;
    public CampHeroSlotUI[] campHeroSlots;



    public override ScreenController Construct()
    {
        return new CampScreenController(this);
    }

}
