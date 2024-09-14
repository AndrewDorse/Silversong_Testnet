using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesScreenView : ScreenView
{
    public UniversalMenuButton[] botButtons; // change to universal ones

    public UniversalMenuButton[] abilityGroupsButtons;
    public UniversalMenuButton[] magicSchoolButtons;
    public UniversalItemUISlot[] abilitySlots;

    public Button activePassiveToggle;

    public TMPro.TMP_Text pointsText;
    public GameObject magicSchoolsPanel;


    public override ScreenController Construct()
    {
        return new AbilitiesScreenController(this);
    }

}
