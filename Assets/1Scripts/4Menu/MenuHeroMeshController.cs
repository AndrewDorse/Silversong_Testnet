using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHeroMeshController : MonoBehaviour
{
    [SerializeField] private HeroMesh _heroMesh;


    private void Start()
    {
        EventsProvider.OnHeroClassSubraceChange += UpdateMesh;

    }


    private void UpdateMesh(HeroClass heroClass, Subrace subrace)
    {
        _heroMesh.SetClassAndRace(heroClass, subrace);
    }


    private void OnDestroy()
    {
        EventsProvider.OnHeroClassSubraceChange -= UpdateMesh;
    }
}
