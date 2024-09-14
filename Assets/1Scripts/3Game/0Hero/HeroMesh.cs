using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMesh : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer face, hair, helmet,
                     chest, shoulderPad, gloves, shoes,
                     belt, backPack
                      ;

    [SerializeField] private MeshRenderer rightHand, leftHand, shield;



    [SerializeField] private Material[] _materials;

    [SerializeField] private Collider[] _weaponColliders; // 0 - right, 1 - left, 2 - common in front

    [SerializeField] private Animator _animator;

    [SerializeField] private WeaponController[] _weaponControllers;



    public void SetClassAndRace(HeroClass heroClass, Subrace subrace)
    {
        _animator.runtimeAnimatorController = heroClass.animatorController;

        SetSubRace(subrace);
        SetSubraceScale(subrace.scale);

        if (heroClass.classMeshes.helmetType == Enums.ClassHelmTypes.none)
        {
            face.sharedMesh = subrace.faceMesh;
            face.gameObject.SetActive(true);
            helmet.gameObject.SetActive(false);
            hair.gameObject.SetActive(true);
            hair.sharedMesh = subrace.hairMesh;
        }
        else if (heroClass.classMeshes.helmetType == Enums.ClassHelmTypes.openFaceHelm)
        {
            if (heroClass.classMeshes.helmet != null)
            {
                helmet.sharedMesh = heroClass.classMeshes.helmet;
                helmet.gameObject.SetActive(true);
                face.gameObject.SetActive(true);
                hair.sharedMesh = subrace.halfHairMesh;
            }
        }
        else if (heroClass.classMeshes.helmetType == Enums.ClassHelmTypes.fullHelmet)
        {
            if (heroClass.classMeshes.helmet != null)
            {
                helmet.sharedMesh = heroClass.classMeshes.helmet;
                helmet.gameObject.SetActive(true);
                face.gameObject.SetActive(false);
                hair.gameObject.SetActive(false);
            }
        }
        else if (heroClass.classMeshes.helmetType == Enums.ClassHelmTypes.mask)
        {
            if (heroClass.classMeshes.helmet != null)
            {
                helmet.sharedMesh = heroClass.classMeshes.helmet;
                helmet.gameObject.SetActive(true);
                face.gameObject.SetActive(true);
                hair.gameObject.SetActive(true);
                hair.sharedMesh = subrace.hairMesh;
            }
        }



        if (heroClass.classMeshes.leftHand != null)
        {
            leftHand.GetComponent<MeshFilter>().sharedMesh = heroClass.classMeshes.leftHand;
            leftHand.gameObject.SetActive(true);
        }
        else leftHand.gameObject.SetActive(false);

        if (heroClass.classMeshes.rightHand != null)
        {
            rightHand.GetComponent<MeshFilter>().sharedMesh = heroClass.classMeshes.rightHand;
            rightHand.gameObject.SetActive(true);
        }
        else rightHand.gameObject.SetActive(false);

        chest.sharedMesh = heroClass.classMeshes.chest;
        shoulderPad.sharedMesh = heroClass.classMeshes.shoulderPad;
        gloves.sharedMesh = heroClass.classMeshes.gloves;
        shoes.sharedMesh = heroClass.classMeshes.shoes;

        if (heroClass.classMeshes.shield != null)
        {
            shield.gameObject.SetActive(true);
            shield.GetComponent<MeshFilter>().sharedMesh = heroClass.classMeshes.shield;
        }
        else shield.gameObject.SetActive(false);

        if (heroClass.classMeshes.belt != null)
        {
            belt.gameObject.SetActive(true);
            belt.sharedMesh = heroClass.classMeshes.belt;
        }
        else belt.gameObject.SetActive(false);
    }


    private void SetSubRace(Subrace subrace)
    {
        face.sharedMesh = subrace.faceMesh;

        if (!helmet.gameObject.activeInHierarchy)
        {
            hair.sharedMesh = subrace.hairMesh;
        }
        else
        {
            hair.sharedMesh = subrace.halfHairMesh;

        }
    }

    private void SetSubraceScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public void SetMaterial(int materialNumber)
    {
        Material material = _materials[materialNumber];

        face.material = material;
        hair.material = material;
        helmet.material = material;
        chest.material = material;
        shoulderPad.material = material;
        gloves.material = material;
        shoes.material = material;

        rightHand.material = material;
        leftHand.material = material;
        shield.material = material;
    }


    public void TurnOffWeaponControllers()
    {
        _weaponControllers[0].gameObject.SetActive(false);
        _weaponControllers[1].gameObject.SetActive(false);
        _weaponControllers[2].gameObject.SetActive(false);
    }

}



[System.Serializable]
public class HeroClassMeshes
{
    public Mesh face, hair, helmet,
                   chest, shoulderPad, gloves, shoes,
                   belt, backPack, backShield, backWeapon,
                   rightHand, leftHand, shield;

    public Enums.ClassHelmTypes helmetType = Enums.ClassHelmTypes.none;
}