using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Race")]
public class Race : ScriptableObject
{
    public int id;
    public Enums.Races race;
    public Subrace[] subraces;
    public Sprite icon;
    public string raceName;
    public string description;

}
