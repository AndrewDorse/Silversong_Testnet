using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable/HeroClass")]

public class HeroClass : ScriptableObject
{
    public int id;
    public string className;

    [Tooltip("Stats")]
    public Enums.MainAttributes attribute;



#if UNITY_EDITOR
    [PreviewSprite]
#endif
    public Sprite icon, portrait;




    public RuntimeAnimatorController animatorController;

    public HeroClass[] nextClasses;

    public HeroClassMeshes classMeshes;


    public List<ActiveAbility> ActiveAbilities;
    public List<PassiveAbility> PassiveAbilities;
}
