using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuComponents : MonoBehaviour
{
    public static MenuComponents instance;

    public GameObject HeroObject => _heroObject;

    [SerializeField] private GameObject _heroObject;


    private void Awake()
    {
        instance = this;
    }
}
