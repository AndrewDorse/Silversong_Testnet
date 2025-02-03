using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalParameter : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _nameText;
    [SerializeField] private TMPro.TMP_Text _valueText;

    [SerializeField] private Button _button;




    public void SetupStat(Enums.Stats stat, float value)
    {
        gameObject.SetActive(true);

        _nameText.text = stat.ToString();
        _valueText.text = value.ToString();
    }
}
