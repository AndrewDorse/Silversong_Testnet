using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsSlotUI : MonoBehaviour
{
    [SerializeField] private Image classIcon;
    [SerializeField] private TMPro.TMP_Text playerText;

    [SerializeField] private TMPro.TMP_Text damageText;
    [SerializeField] private TMPro.TMP_Text tankText;
    [SerializeField] private TMPro.TMP_Text healText;
    [SerializeField] private TMPro.TMP_Text controlText;
    [SerializeField] private TMPro.TMP_Text casterText;


    [SerializeField] private GameObject damageBest;
    [SerializeField] private GameObject tankBest;
    [SerializeField] private GameObject healBest;
    [SerializeField] private GameObject controlBest;
    [SerializeField] private GameObject casterBest;





    public void Setup(Silversong.Game.Statistics.PlayerStatisticsSlot data)
    {

        classIcon.sprite = DataProvider.HeroClassProviderData.GetHeroClass(data.ClassId).icon;

        playerText.text = data.Nickname;

        damageText.text = data.heroData[0].Value.ToString();
        damageBest.SetActive(data.heroData[0].BestResult);

        tankText.text = data.heroData[1].Value.ToString();
        tankBest.SetActive(data.heroData[1].BestResult);

        healText.text = data.heroData[2].Value.ToString();
        healBest.SetActive(data.heroData[2].BestResult);

        controlText.text = data.heroData[3].Value.ToString();
        controlBest.SetActive(data.heroData[3].BestResult);

        casterText.text = data.heroData[4].Value.ToString();
        casterBest.SetActive(data.heroData[4].BestResult);

    }


}
