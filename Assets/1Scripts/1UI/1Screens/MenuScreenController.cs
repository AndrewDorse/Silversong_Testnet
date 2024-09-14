using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenController : ScreenController
{
    private readonly MenuScreenView _view;

    public MenuScreenController(MenuScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        _view.quickStartButton.onClick.AddListener(QuickStart);
    }

    private void QuickStart()
    {
        Master.instance.StartQuickGame();
    }



}
