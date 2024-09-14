using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalMenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _buttonRect;
    [SerializeField] private Image _image;
    [SerializeField] private Image _frame;

    [SerializeField] private Sprite[] _sprites;

    [SerializeField] private Vector2[] _sizes;

    [SerializeField] private Color[] _colors;

    public void Chosen(bool value)
    {
        if (value)
        {
            if (_sprites.Length == 2)
            {
                _image.sprite = _sprites[0];
            }

            _buttonRect.sizeDelta = _sizes[0];
        }
        else
        {
            if (_sprites.Length == 2)
            {
                _image.sprite = _sprites[1];
            }

            _buttonRect.sizeDelta = _sizes[1];
        }
    }

    public void Setup(int id, Action<int> callback)
    {
        _button.onClick.RemoveAllListeners();

        _button.onClick.AddListener(() => callback?.Invoke(id));
    }

    public void Setup(Action callback)
    {
        _button.onClick.RemoveAllListeners();

        _button.onClick.AddListener(() => callback?.Invoke());
    }
}
