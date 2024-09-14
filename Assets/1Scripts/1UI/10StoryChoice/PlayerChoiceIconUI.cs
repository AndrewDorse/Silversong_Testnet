using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoiceIconUI : MonoBehaviour
{
    [SerializeField] private Image _classIcon;
    [SerializeField] private TMPro.TMP_Text _nicknameText;


    public void Setup(string nickname, Sprite icon)
    {
        gameObject.SetActive(true);

        _classIcon.sprite = icon;
        _nicknameText.text = nickname;
    }


}
