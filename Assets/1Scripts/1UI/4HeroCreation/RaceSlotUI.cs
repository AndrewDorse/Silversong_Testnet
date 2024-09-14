using System;
using UnityEngine;
using UnityEngine.UI;

public class RaceSlotUI : MonoBehaviour
{
    [SerializeField] private Image _portrait; 
    [SerializeField] private Image _frame;
    [SerializeField] private Button _button;

    //private bool _choosen;

    private Race _race;
    private Subrace _subrace;

    public void Setup(Race race, Action<Race, RaceSlotUI> callback )
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback.Invoke(_race, this));

        _portrait.sprite = race.icon;

        _race = race;
        // frame ?

        _frame.gameObject.SetActive(false);
    }

    public void Setup(Subrace subrace, Action<Subrace, RaceSlotUI> callback)
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => callback.Invoke(_subrace, this));

        _portrait.sprite = subrace.icon;

        _subrace = subrace;

        // frame ?

        _frame.gameObject.SetActive(false);
    }

    public void Choose(bool value)
    {
        _frame.gameObject.SetActive(value);
    }

}
