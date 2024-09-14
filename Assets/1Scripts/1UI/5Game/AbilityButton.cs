using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image _icon;


    [SerializeField] private TMPro.TMP_Text _cooldownText;


    [SerializeField] private Color _cooldownColor;

    [SerializeField] private Button _button;


    private Action<int> _callbackPressed;
    private Action<int> _callbackReleased;
    private int _buttonId;



    public void Setup(ActiveAbility activeAbility, Action<int> callbackPressed, Action<int> callbackReleased, int buttonId)
    {
        _buttonId = buttonId;

        _callbackPressed = callbackPressed;
        _callbackReleased = callbackReleased;

        _icon.sprite = activeAbility.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _callbackPressed?.Invoke(_buttonId);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _callbackReleased?.Invoke(_buttonId);
    }

}
