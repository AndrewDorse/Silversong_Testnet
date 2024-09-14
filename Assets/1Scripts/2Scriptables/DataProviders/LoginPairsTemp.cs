using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/LoginPairsTemp")]
public class LoginPairsTemp : ScriptableObject
{
    public LoginPair[] list; 


    public bool Login(string userName, string password)
    {
        foreach(LoginPair loginPair in list)
        {
            if(loginPair.nickname == userName)
            {
                if(loginPair.password == password)
                {
                    return true;
                }
            }
        }

        return false;
    }

}

[System.Serializable]
public class LoginPair
{
    public string nickname;
    public string password;
}
