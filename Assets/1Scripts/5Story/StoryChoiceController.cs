using Silversong.Base;
using Silversong.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChoiceController  
{ 
    public static Story GetStory()
    {
        int id = DataController.instance.GameData.GameLevels[DataController.instance.GameData.Level - 1].StoryId;

        return InfoProvider.instance.GetStory(id);
    }

    public StoryChoiceController()
    {
        EventsProvider.OnLevelStart += OnStartLevel;
        EventsProvider.OnPlayersDataChanged += CheckPlayersChoices;


        
    }





    private void OnStartLevel()
    {
        LevelSlot slot = DataController.instance.GameData.GameLevels[DataController.instance.GameData.Level - 1];

        if (slot.LevelType == Enums.LevelType.StoryChoice)
        {
            SetStoryChoice(slot.StoryId);
        }
    }
    

    private void SetStoryChoice(int id)
    {
        Master.instance.ChangeGameStage(Enums.GameStage.StoryChoice);
    }

    private void CheckPlayersChoices(List<PlayerData> players)
    {
        if (MainRPCController.instance.IsMaster == false)
        {
            return;
        }


        int[] results = new int[5];



        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].StoryChoice == -1)
            {
                return;
            }

            results[players[i].StoryChoice]++;
        }


        OnAllPlayerMadeAChoice(results);
    }

    private void OnAllPlayerMadeAChoice(int[] results)
    {
        

        if (MainRPCController.instance.IsMaster == false)
        {
            return;
        }

        List<int> optionNumbers = new List<int>();

        GetMostVotedOptions(results, optionNumbers);

        int result = optionNumbers[Random.Range(0, optionNumbers.Count)];


        GameMaster.RewardsController.SetStoryOptionRewards(GetStory().Options[result].RewardSlot);

        EventsProvider.OnAllPlayersMadeChoice?.Invoke(result);

        DataController.LocalPlayerData.StoryChoice = -1;

        Debug.Log("#OnAllPlayerMadeAChoice# " + results[0] + results[1] + results[2] + results[3] + results[4] + "  " + result + "  most voted anount" + optionNumbers.Count);
    }

    private static void GetMostVotedOptions(int[] results, List<int> optionNumbers)// finding most voted option / options
    {
        int maxVotes = 0;

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] > maxVotes)
            {
                optionNumbers.Clear();
                optionNumbers.Add(i);
                maxVotes = results[i];
            }
            else if (results[i] == maxVotes && results[i] > 0)
            {
                optionNumbers.Add(i);
            }
        }
    }
}



