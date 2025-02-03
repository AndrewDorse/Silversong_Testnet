using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InRoomScreenView : ScreenView
{
    public Button heroCreateButton;
    public Button leaveRoomButton;
    public Button readyButton;
    public Button addBotButton;

    public PlayerSlotInRoom[] playerSlotsInRoom;
    public Transform playersListTransform;

    public override ScreenController Construct()
    {
        return new InRoomScreenController(this);
    }

}
