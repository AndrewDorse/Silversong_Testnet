using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneChangeCallbacks
{
    public static (Action, bool) GetCallback(Enums.GameStage currentStage, Enums.GameStage nextStage)
    {
        Action callback = null;
        bool needToChangeScene = false;

        if (currentStage == Enums.GameStage.launch && nextStage == Enums.GameStage.login)
        {
            callback = LoadedLoginCallback;
            needToChangeScene = true;
        }
        else if(currentStage == Enums.GameStage.login && nextStage == Enums.GameStage.menu)
        {
            callback = LoadedMenuCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.menu && nextStage == Enums.GameStage.inRoom)
        {
            callback = LoadedInRoomCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.inRoom && nextStage == Enums.GameStage.heroCreation)
        {
            callback = LoadedHeroCreationCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.inRoom && nextStage == Enums.GameStage.menu) // leave room
        {
            callback = LeaveRoomCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.heroCreation && nextStage == Enums.GameStage.inRoom) // after hero created
        {
            callback = LeaveHeroCreationCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.inRoom && nextStage == Enums.GameStage.game) // start game
        {
            callback = LevelLoadedCallback;
            needToChangeScene = true;
        }
        else if (currentStage == Enums.GameStage.menu && nextStage == Enums.GameStage.game) // reconnect
        {
            callback = LevelLoadedCallback;
            needToChangeScene = true;
        }
        else if (currentStage == Enums.GameStage.game && nextStage == Enums.GameStage.statistics) // statistics
        {
            callback = StatisticsCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.statistics && nextStage == Enums.GameStage.rewards) // rewards
        {
            callback = RewardCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.rewards && nextStage == Enums.GameStage.camp) // to the camp
        {
            callback = CampCallback;
            needToChangeScene = true;
        }


        // inside camp
        else if (currentStage == Enums.GameStage.camp && nextStage == Enums.GameStage.inventory) // inventory / shop
        {
            callback = InventoryCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.camp && nextStage == Enums.GameStage.abilities) // abilities
        {
            callback = AbilitiesCallback;
            needToChangeScene = false;
        }



        else if (currentStage == Enums.GameStage.inventory && nextStage == Enums.GameStage.camp) // camp
        {
            callback = CampCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.inventory && nextStage == Enums.GameStage.abilities) // abilities
        {
            callback = AbilitiesCallback;
            needToChangeScene = false;
        }

        else if (currentStage == Enums.GameStage.abilities && nextStage == Enums.GameStage.camp) // camp
        {
            callback = CampCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.abilities && nextStage == Enums.GameStage.inventory) // abilities
        {
            callback = InventoryCallback;
            needToChangeScene = false;
        }

        // new level

        else if (currentStage == Enums.GameStage.camp ||
            currentStage == Enums.GameStage.inventory ||
            currentStage == Enums.GameStage.abilities ||
            currentStage == Enums.GameStage.campHeroInfo
            ) 
        {
            if (nextStage == Enums.GameStage.game)
            {
                callback = LevelLoadedCallback;
                needToChangeScene = true;
            }
        }


        // story choice
        else if (currentStage == Enums.GameStage.game && nextStage == Enums.GameStage.StoryChoice)  
        {
            callback = StoryChoiceCallback;
            needToChangeScene = false;
        }
        else if (currentStage == Enums.GameStage.StoryChoice && nextStage == Enums.GameStage.game)
        {
            callback = FromStoryChoiceCallback;
            needToChangeScene = false;
        }




        return (callback, needToChangeScene);
    }


    private static void LoadedLoginCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.login);

    }

    private static void LoadedMenuCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.menu);

    }

    private static void LoadedInRoomCallback()
    {
        Master.instance.RemoveLoadingScreen();
        Master.instance.OpenScreen(Enums.GameStage.inRoom);
       
    }

    private static void LoadedHeroCreationCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.heroCreation);

    }

    private static void LeaveRoomCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.menu);
        Master.instance.LeaveRoom();
    }

    private static void LeaveHeroCreationCallback() // On Ready
    {
        Master.instance.OpenScreen(Enums.GameStage.inRoom);

    }

    private static void LevelLoadedCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.game);

        Master.instance.RemoveLoadingScreen();

        Debug.Log("# LevelLoadedCallback " );

        EventsProvider.OnLevelStart?.Invoke();
    }

    private static void StatisticsCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.statistics);
    }

    private static void RewardCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.rewards);
    }

    private static void CampCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.camp);
    }

    private static void InventoryCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.inventory);
    }
    private static void AbilitiesCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.abilities);
    }



    private static void StoryChoiceCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.StoryChoice);
    }

    private static void FromStoryChoiceCallback()
    {
        Master.instance.OpenScreen(Enums.GameStage.game);
    }
}
