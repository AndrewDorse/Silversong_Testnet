using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyPlayerSlotUI : MonoBehaviour
{
    public string Id => _id;

    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _nicknameText;
    [SerializeField] private Image _hpImage;
    [SerializeField] private Image _mpImage;

    // death image TODO
    private string _id;

    public void Setup(PlayerData data)
    {
        gameObject.SetActive(true);

        _icon.sprite = DataProvider.HeroClassProviderData.GetHeroClass(data.heroData.classId).icon;

        _nicknameText.text = data.nickname;
        _id = data.userId;
    }

    public void SetHpMp(float hp, float mp)
    {
        _hpImage.fillAmount = hp;
        _mpImage.fillAmount = mp;
    }
}
