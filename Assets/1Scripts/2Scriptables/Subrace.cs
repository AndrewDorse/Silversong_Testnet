using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Subrace")]

public class Subrace : ScriptableObject
{
    public int id = 0;
    public string subraceName, subraceSecondName;
    public string description;

    //public EnumsHandler.Tags[] tags;
    

#if UNITY_EDITOR
    [PreviewSprite]
#endif

    public Sprite icon;
    public Vector3 scale;
    public float stepOffset;
    // public EnumsHandler.ItemRareness rareness;

    //public StatArray onSubraceChoose;
    //public Passive[] onSubraceChoosePassives;




    //public Mesh faceMesh;
    //public Mesh hairMesh, halfHairMesh;

    //public Material hairMaterial;
    //public Material bodyMaterial;
    //public Material faceMaterial;

    public MeshHead meshHead;
}
