using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveController
{
    

    public static void SaveLocalData()
    {
        string saveJson = JsonUtility.ToJson(DataController.instance.LocalData);
        PlayerPrefs.SetString("LocalData", saveJson);
    }

    public static LocalData LoadLocalData()
    {
        if (PlayerPrefs.HasKey("LocalData"))
        {
            string saveJson = PlayerPrefs.GetString("LocalData");

            LocalData localData = JsonUtility.FromJson<LocalData>(saveJson);

            return localData;
        }
        else
        {
            return new LocalData();
        }
    }

}
