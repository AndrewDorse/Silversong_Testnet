using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : ScriptableObject
{

    public int Id;

#if UNITY_EDITOR
    [PreviewSprite]
#endif
    public Sprite Icon;

    public string Name;
    public string Description;

}
