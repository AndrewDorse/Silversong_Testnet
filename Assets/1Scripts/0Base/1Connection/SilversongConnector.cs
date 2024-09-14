using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilversongConnector
{
    

    public void OnPhotonConnection()
    {
        // check if we connected to our server


        Master.instance.OnSuccessfullLogin();
    }

}
