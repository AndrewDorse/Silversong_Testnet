using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silversong.Base
{
    public class GameStageController
    {
        private Enums.GameStage _currentStage = Enums.GameStage.launch;

        private SceneController _sceneController = new SceneController();


        public void ChangeStage(Enums.GameStage stage)
        {
            (Action, bool) info = SceneChangeCallbacks.GetCallback(_currentStage, stage);

            Action callback = info.Item1;
            bool needToChangeScene = info.Item2;

            _currentStage = stage;

            if (needToChangeScene)
            {
                int sceneNumber = GetSceneNumberByStage(stage);

                _sceneController.LoadScene(sceneNumber, callback);
            }
            else
            {
                callback?.Invoke();
            }

            
            EventsProvider.OnGameStateChange?.Invoke(_currentStage);
        }

        public void LaunchGameStage(string levelName)
        { 

            (Action, bool) info = SceneChangeCallbacks.GetCallback(_currentStage, Enums.GameStage.game);

            Action callback = info.Item1;
            bool needToChangeScene = info.Item2;


            if (needToChangeScene)
            {
                Master.instance.SetLoadingScreen();
                _sceneController.LoadScene(levelName, callback);
            }
            else
            {
                callback?.Invoke();
            }

            
            _currentStage = Enums.GameStage.game;
            EventsProvider.OnGameStateChange?.Invoke(_currentStage);
        }

        private int GetSceneNumberByStage(Enums.GameStage stage)
        {
            int result = 1;

            if(stage == Enums.GameStage.login || 
                stage == Enums.GameStage.menu || 
                stage == Enums.GameStage.camp ||
                stage == Enums.GameStage.inRoom ||
                stage == Enums.GameStage.heroCreation ||
                stage == Enums.GameStage.heroUpgrade)
            {
                result = 1;
            }
            
            

            return result;
        }

    }
}