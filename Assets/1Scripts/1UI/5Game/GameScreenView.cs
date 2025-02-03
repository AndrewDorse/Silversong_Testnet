using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenView : ScreenView
{
    public Joystick joystick;



    public AbilityButton[] abilityButtons;
    public Button statsButton;

    public Image hpImage;
    public Image mpImage;


    public PartyPlayerSlotUI[] partyPlayerSlotUIs;

    public override ScreenController Construct()
    {
        return new GameScreenController(this);
    }

}
