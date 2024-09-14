using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Base
{
    public class Launcher : MonoBehaviour
    {


        void Start()
        {
            DontDestroyOnLoad(this);

            Debug.unityLogger.logEnabled = true;

            Master.instance.ChangeGameStage(Enums.GameStage.login);
        }

    }
}