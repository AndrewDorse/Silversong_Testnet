using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController
{
    private ScreenView _view;


    public void Open() { }

    public void Close() 
    {

        Dispose();
        _view.CloseScreen();


    }

    public ScreenController(ScreenView view)
    {
        _view = view;
    }


    public virtual void Dispose()
    {

    }

   

}
