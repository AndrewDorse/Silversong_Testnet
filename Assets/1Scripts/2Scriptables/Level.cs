using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Level")]

public class Level : ScriptableObject
{
    public int Id;
    public string AddressablesName;

    public Enums.Region Region;



}
