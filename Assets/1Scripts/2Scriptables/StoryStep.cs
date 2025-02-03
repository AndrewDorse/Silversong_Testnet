using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/StoryStep")]
public class StoryStep : ScriptableObject
{
    public string Text;
    public Sprite Icon;
   

    public StoryStepOption[] Options;
}
