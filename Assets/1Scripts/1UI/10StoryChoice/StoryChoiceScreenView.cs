using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryChoiceScreenView : ScreenView
{
    public TMPro.TMP_Text mainText;
    public ChoiceSlotUI[] options;
    public Image storyImage;
    public Button continueButton;

    public override ScreenController Construct()
    {
        return new StoryChoiceScreenController(this);
    }

}
