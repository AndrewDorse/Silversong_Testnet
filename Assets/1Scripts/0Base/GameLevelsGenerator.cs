using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Base
{
    public class GameLevelsGenerator
    {
        public GameLevelsGenerator()
        {
            EventsProvider.OnJoinRoom += OnMasterCreatedRoom;

        }

        private void OnMasterCreatedRoom()
        {
            if(MainRPCController.instance.IsMaster)
            {
                Generate();
                EventsProvider.OnJoinRoom -= OnMasterCreatedRoom;
            }
        }


        private void Generate()
        {
            int maxLevel = 25;

            LevelSlot[] gameLevels = new LevelSlot[maxLevel];

            for (int i = 0; i < maxLevel; i++)
            {

                LevelSlot slot = new LevelSlot();
                slot.LevelNumber = i + 1;
                slot.LevelType = GetLevelType(i, 0, 0, 0);


                (slot.LevelId, slot.Region) = GetLevelId(0, null);

                gameLevels[i] = slot;


            }


            DataController.instance.GameData.GameLevels = gameLevels;

        }

        private Enums.LevelType GetLevelType(int currentLevel, int battlesAmount, int choicesAmount, int scenarioAmount) // TODO
        {

            // if it is choice we need story ID

            return (Enums.LevelType)Random.Range(0, 2);
        }

        private (int, Enums.Region) GetLevelId(int currentLevel, Enums.Region[] lastLevelRegions) // TODO + last levels ids too
        {
            return (Random.Range(0, 2), (Enums.Region)Random.Range(0, 2));
        }


    }






    [System.Serializable]
    public class LevelSlot
    {
        public int LevelNumber;
        public int LevelId;
        public Enums.LevelType LevelType;
        public Enums.Region Region;
        public int StoryId;





    }










}