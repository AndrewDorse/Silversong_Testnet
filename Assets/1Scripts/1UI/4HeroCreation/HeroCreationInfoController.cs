using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCreationInfoController : MonoBehaviour
{
    [SerializeField] private Image _portrait;
    [SerializeField] private Image _frame;
    [SerializeField] private Image _icon;





    public void SetHeroClass(HeroClass heroClass)
    {
        _portrait.sprite = heroClass.portrait;
        _icon.sprite = heroClass.icon;
        _icon.gameObject.SetActive(true);
    }

    public void SetSubrace(Subrace subrace)
    {
        _portrait.sprite = subrace.icon;


        _icon.gameObject.SetActive(false);

    }
}
