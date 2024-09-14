using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSlotUI : MonoBehaviour
{

    [SerializeField] private Image _icon;
    [SerializeField] private TMPro.TMP_Text _optionText;

    [SerializeField] private PlayerChoiceIconUI[] _playersChoices;

    [SerializeField] private Button _rewardsButton;
    [SerializeField] private Button _chooseOptionButton;

    [SerializeField] private Image[] _rewardsIcons;
    [SerializeField] private GameObject _unknownRewardIcon;
    [SerializeField] private GameObject _rewardsViewGo;



    public void Setup(StoryOption storyOption, Action<int> rewardsCallback, Action<int> choiceCallback, int number )
    {
        gameObject.SetActive(true);
       

        _rewardsButton.onClick.AddListener(() => rewardsCallback?.Invoke(number));
        _chooseOptionButton.onClick.AddListener(() => choiceCallback?.Invoke(number));


        _icon.sprite = storyOption.Icon;
        _optionText.text = storyOption.Text;



        SetRewardsView(storyOption, rewardsCallback != null);
    }

    public void ClearChoices()
    {
        foreach(PlayerChoiceIconUI choiceIcon in _playersChoices)
        {
            choiceIcon.gameObject.SetActive(false);
        }
    }

    public void AddUserStoryChoice(int heroClassId, string nickName)
    {
        for (int i = 0; i < _playersChoices.Length; i++)
        {
            PlayerChoiceIconUI choiceIcon = _playersChoices[i];

            if(choiceIcon.gameObject.activeInHierarchy)
            {
                continue;
            }


            Sprite classIcon = InfoProvider.instance.GetHeroClass(heroClassId).icon;

            choiceIcon.Setup(nickName, classIcon);

            break; 
        }
    }


    private void SetRewardsView(StoryOption storyOption, bool showRewards)
    {
        if(showRewards)
        {
            _unknownRewardIcon.SetActive(false);
            _rewardsViewGo.SetActive(true);

            for(int i =0; i < _rewardsIcons.Length; i++)
            {


                if(i < storyOption.RewardSlot.Length)
                {
                    _rewardsIcons[i].sprite = InfoProvider.instance.RewardsProviderData.GetIconByRewardType(storyOption.RewardSlot[i].RewardType, storyOption.RewardSlot[i].Value);
                    _rewardsIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    _rewardsIcons[i].gameObject.SetActive(false);

                }

            }

        }
        else
        {
            _unknownRewardIcon.SetActive(true);
            _rewardsViewGo.SetActive(false);
        }







    }


}
