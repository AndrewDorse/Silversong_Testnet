using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassSlotUI : MonoBehaviour
{
    [SerializeField] private Image _frame;
    [SerializeField] private Image _icon;

    [SerializeField] private Button _button;
    private HeroClass _heroClass;


    public void Setup(HeroClass heroClass, Action<HeroClass> callback)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback.Invoke(_heroClass));

        _heroClass = heroClass;

        _icon.sprite = heroClass.icon;

    }
}
