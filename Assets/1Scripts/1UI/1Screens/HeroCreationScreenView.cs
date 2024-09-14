using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCreationScreenView : ScreenView
{
    public ClassSlotUI[] heroClassChoices;
    public ClassSlotUI[] heroClassNextChoices;

    public RaceSlotUI[] raceSlots;
    public RaceSlotUI[] subraceSlots;

    public GameObject classVariant;
    public GameObject raceVariant;

    public Button[] topButtons;

    public HeroCreationInfoController heroInfoController;
    public HeroCreationOverviewPage heroCreationOverviewPage;

    public UIPage[] uIPages;


    public override ScreenController Construct()
    {
        return new HeroCreationScreenController(this);
    }





}
