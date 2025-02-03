using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Story")]

public class Story : ScriptableObject
{
    public int Id = 0;
    

    public StoryStep StartingStep;



}
 

[System.Serializable]
public class StoryStepOption
{
    public string Text;
    public Sprite Icon;

    public bool ShowRewards = true;

    public Enums.Tag RequiredTag;

    public StoryStep NextStoryStep;

    public RewardSlot[] RewardSlot;

    public BattleSlot BattleSlot;

    public string ResultText;
}


[System.Serializable]
public class RewardSlot
{
    public Enums.RewardType RewardType;
    public Enums.RewardReciever RecieverType;
    public Enums.Tag RequiredTag;
    public int Value; 
}


[System.Serializable]
public class BattleSlot
{
    public List<Mob> mobs;
}