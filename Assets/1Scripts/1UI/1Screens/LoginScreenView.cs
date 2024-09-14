using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenView : ScreenView
{
    public Button loginButton;
    public Button serverButton;
    public Button closeServerButton;


    public GameObject serversWindow;
    public ServerSlotUI[] serverSlots;

    public Transform listTransform;



    public override ScreenController Construct()
    {
        return new LoginScreenController(this);
    }

}
