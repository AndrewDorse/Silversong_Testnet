using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Game.Statistics
{ 
    public class LevelStatisticsController  // OVERDAMAGE COUNT REMOVE
    {

        private List<PlayerStatisticsSlot> _playersStatistics;


        public LevelStatisticsController()
        {
            Subcribe();

            _playersStatistics = new List<PlayerStatisticsSlot>();
        }

        private void Subcribe()
        {
            EventsProvider.OnEnemyRecievedDamage += AddLocalPlayerDamageStatisticsData; // local

            EventsProvider.EveryFiveSeconds += SendLocalStatisticsToOthers;
            EventsProvider.OnStatisticsRpcRecieved += AddStatisticsDataFromRpc;

            EventsProvider.OnDeathOfAllEnemies += OnDeathOfAllEnemies;
        }



        public List<PlayerStatisticsSlot> GetLevelStatistics()
        {
            GetBestResultsIds();

            return _playersStatistics;
        }






        private void SendLocalStatisticsToOthers()
        {
            GameRPCController.instance.SendLocalStatisticsToOthers(JsonUtility.ToJson(new LevelStatisticsData(_playersStatistics)));
        }



        private void AddStatisticsDataFromRpc(LevelStatisticsData data)
        {
            foreach(PlayerStatisticsSlot playerSlot in data.playersData)
            {
                UpdateOtherPlayerStatistics(playerSlot);
            }
        }

        private void UpdateOtherPlayerStatistics(PlayerStatisticsSlot newPlayerSlotData)
        {
            for (int i = 0; i < _playersStatistics.Count; i++)
            {
                if (_playersStatistics[i].UserId == newPlayerSlotData.UserId)
                {
                    _playersStatistics[i] = newPlayerSlotData;
                    return;
                }
            }

            _playersStatistics.Add(newPlayerSlotData);
        }

        private void OnDeathOfAllEnemies()
        {

            SendLocalStatisticsToOthers();

            Dispose();
        }






















        // LOCAL statistics
        private void AddLocalPlayerDamageStatisticsData(string attackerId, string attackedId, float value)
        {
            AddPlayerStatisticsData(attackerId, Enums.StatisticsType.damage, value);
        }

        private void AddPlayerStatisticsData(string userId, Enums.StatisticsType type, float value)
        {
            if (_playersStatistics == null)
            {
                _playersStatistics = new List<PlayerStatisticsSlot>();
            }

            PlayerStatisticsSlot playerStatisticsSlot = GetPlayerStatistics(userId);
            playerStatisticsSlot.heroData[(int)type].Value += value;

            

        }

        private PlayerStatisticsSlot GetPlayerStatistics(string userId)
        {
            foreach (PlayerStatisticsSlot playerSlot in _playersStatistics)
            {
                if (playerSlot.UserId == userId)
                {
                    return playerSlot;
                }
            }

            // no player slot yet
            PlayerStatisticsSlot newPLayerSlot = new PlayerStatisticsSlot(userId);
            _playersStatistics.Add(newPLayerSlot);

            return _playersStatistics[_playersStatistics.Count - 1];
        }





        // sort at the end of level

        private void GetBestResultsIds()
        {

            GetBestPlayerByStatisticsType(Enums.StatisticsType.damage);
            GetBestPlayerByStatisticsType(Enums.StatisticsType.tank);
            GetBestPlayerByStatisticsType(Enums.StatisticsType.heal);
            GetBestPlayerByStatisticsType(Enums.StatisticsType.control);
            GetBestPlayerByStatisticsType(Enums.StatisticsType.caster);
        }

        private string GetBestPlayerByStatisticsType(Enums.StatisticsType type)
        {
            string bestResultUserId = string.Empty;
            float value = 0;
            StatisticsSlot bestSlot = null;


            foreach (PlayerStatisticsSlot playerSlot in _playersStatistics)
            {
                if(playerSlot.heroData[(int)type].Value > value)
                {
                    bestResultUserId = playerSlot.UserId;
                    value = playerSlot.heroData[(int)type].Value;
                    bestSlot = playerSlot.heroData[(int)type];
                }
            }

            if (value > 0)
            {
                bestSlot.BestResult = true;
            }


            return bestResultUserId;
        }













        // dispose
        public void Dispose()
        {
            EventsProvider.OnEnemyRecievedDamage -= AddLocalPlayerDamageStatisticsData; // local


            EventsProvider.EveryFiveSeconds -= SendLocalStatisticsToOthers;
            EventsProvider.OnStatisticsRpcRecieved -= AddStatisticsDataFromRpc;

            EventsProvider.OnDeathOfAllEnemies -= OnDeathOfAllEnemies;
        }

    }


    [System.Serializable]
    public class LevelStatisticsData // for rpc ? hm
    {
        public List<PlayerStatisticsSlot> playersData;

        public LevelStatisticsData(List<PlayerStatisticsSlot> playersData)
        {
            this.playersData = playersData;
        }
    }

    [System.Serializable]
    public class PlayerStatisticsSlot
    {
        public string UserId;
        public string Nickname;

        public int ClassId;
        public StatisticsSlot[] heroData;

        public PlayerStatisticsSlot(string userId)
        {
            UserId = userId;
            Nickname = DataController.instance.LocalData.Nickname;
            ClassId = DataController.instance.GetMyHeroData().classId;


            heroData = new StatisticsSlot[5];
            heroData[0] = new StatisticsSlot(Enums.StatisticsType.damage, 0);
            heroData[1] = new StatisticsSlot(Enums.StatisticsType.tank, 0);
            heroData[2] = new StatisticsSlot(Enums.StatisticsType.heal, 0);
            heroData[3] = new StatisticsSlot(Enums.StatisticsType.control, 0);
            heroData[4] = new StatisticsSlot(Enums.StatisticsType.caster, 0);
        }
    }



    [System.Serializable]
    public class StatisticsSlot
    {
        public Enums.StatisticsType Type;
        public float Value;
        public bool BestResult = false;


        public StatisticsSlot(Enums.StatisticsType type, float value)
        {
            Type = type;
            Value = value;
        }
    } 


}