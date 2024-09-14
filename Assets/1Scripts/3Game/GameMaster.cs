using Photon.Pun;
using Silversong.Game.Scene;
using Silversong.Game.Statistics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Silversong.Game
{
    public class GameMaster : MonoBehaviour
    {
        public static GameMaster instance;

        public List<OtherHero> OtherHeroes;
        public List<Enemy> Enemies => _enemiesController.Enemies;
        public LocalHero LocalHero { get; private set; }





        [SerializeField] private PlayerCreator _playerCreator;
        [SerializeField] private EnemiesController _enemiesController;
        [SerializeField] private GameCameraController _gameCamera;

        private LevelStatisticsController _statisticsController;

        private OtherHeroesController _otherHeroesController = new OtherHeroesController();
        private RewardsController _rewardsController;
        private StoryChoiceController _storyController;


        public static RewardsController RewardsController => instance._rewardsController;



        private void Awake()
        {
            instance = this;

            _rewardsController = new RewardsController();
            _storyController = new StoryChoiceController();

            EventsProvider.OnLevelEnd += LevelEnd;
        }

        public void LevelStart(List<PlayerData> playersData) // enemies info here???? TODO
        {
            LocalHero = CreateLocalHero(DataController.instance.GetMyHeroData());

            _gameCamera.Setup(LocalHero.gameObject);

            OtherHeroes = CreateOtherHeroes(playersData);

            EventsProvider.ThreeTimesPerSecond += SendLocalHeroDataToOthers;


            _statisticsController = new LevelStatisticsController();


            if (PhotonNetwork.IsMasterClient)
            {
                
            }
            else
            {
                GameRPCController.instance.AskMasterForEnemiesInfo();
            }


            EventsProvider.OnDeathOfAllEnemies += OnDeathOfAllEnemies;
            EventsProvider.OnAllPlayersMadeChoice += OnPlayersMadeStoryChoice;
        }

        private void LevelEnd()
        {
            _gameCamera.gameObject.SetActive(false);
            _statisticsController.Dispose();
        }


        public EnemiesData GetEnemiesData()
        {
            return _enemiesController.GetCurrentEnemiesDataForRPC();
        }


        public List<PlayerStatisticsSlot> GetLevelStatistics()
        {
            return _statisticsController.GetLevelStatistics();
        }

        public void SetSpawnPoints(SpawnsInfo  info)
        {
            _enemiesController.SetSpawnPoints(info);

            if (PhotonNetwork.IsMasterClient)
            {
                if (DataController.LevelSlot.LevelType == Enums.LevelType.Battle)
                {
                    _enemiesController.CreateEnemies();
                }
            }
        }



















        private LocalHero CreateLocalHero(HeroData heroData)
        {
            return _playerCreator.CreateLocalHero(heroData);
        }

        private List<OtherHero> CreateOtherHeroes(List<PlayerData> playersData)
        {
            List<OtherHero> result = new List<OtherHero>();

            foreach (PlayerData data in playersData)
            {
                if (data.userId != DataController.instance.LocalData.UserId)
                {
                    result.Add(_playerCreator.CreateOtherHero(data.heroData, data.userId, data.nickname));
                }
            }

            return result;
        }

        private void SendLocalHeroDataToOthers() //TODO Check it
        {
            GameRPCController.instance.UpdateLocalHeroPosition(LocalHero);
        }




        private void OnDeathOfAllEnemies()
        {
            // temp   // change scene on exit collider




            EventsProvider.OnLevelEnd?.Invoke();
            LevelDispose();
            Master.instance.ChangeGameStage(Enums.GameStage.statistics);
            DataController.instance.GameData.Level++;
        }

        private void OnPlayersMadeStoryChoice(int optionNumber)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _enemiesController.CreateStoryEnemies(InfoProvider.instance.GetStory(DataController.LevelSlot.StoryId).Options[optionNumber]);
                
                GameRPCController.instance.SendFinalChoiceToOthers(optionNumber);
            }
            
            DataController.LocalPlayerData.StoryChoice = -1;
        }

       



        private void LevelDispose()  // on exit level
        {
            EventsProvider.ThreeTimesPerSecond -= SendLocalHeroDataToOthers;
            EventsProvider.OnDeathOfAllEnemies -= OnDeathOfAllEnemies;
            EventsProvider.OnAllPlayersMadeChoice -= OnPlayersMadeStoryChoice;

        }


    }
}