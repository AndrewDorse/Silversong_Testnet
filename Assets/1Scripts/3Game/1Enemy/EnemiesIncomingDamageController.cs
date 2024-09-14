using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;



namespace Silversong.Game
{
    public class EnemiesIncomingDamageController  // only for Others? maybe statistics too ??? TODO
    {
        private List<EnemyRecievedDamageData> _allDamageList;



        public EnemiesIncomingDamageController()
        {
            EventsProvider.OnLevelStart += Subcribe;
            EventsProvider.OnLevelEnd += Dispose;

        }

        private void Subcribe()
        {
            EventsProvider.OnEnemyRecievedDamage += DamageRecieved;
            EventsProvider.EverySecond += TimeTick;
            EventsProvider.OnEnemyDeathRpcRecieved += RemoveEnemyData;


            EventsProvider.OnAllEnemiesRecievedDamageDataRpc += DamageInfoFromOtherPlayerRecieved;
        }


        public void DamageRecieved(string attackingId, string attackedId, float damageAmount)
        {   
            // no list
            if (_allDamageList == null)
            {
                _allDamageList = new List<EnemyRecievedDamageData>();

                EnemyRecievedDamageData newData = new EnemyRecievedDamageData(
                    attackedId,
                    new List<EnemyRecievedDamageSlot>() {
                    new EnemyRecievedDamageSlot(attackingId, damageAmount)
                       }
                    );

                _allDamageList.Add(newData);
                return;
            }

            // enemy in list
            foreach (EnemyRecievedDamageData data in _allDamageList)
            {
                if (data.enemyId == attackedId)
                {
                    AddDamageSlot(data, attackingId, damageAmount);
                    return;
                }
            }

            // no enemy in list
            EnemyRecievedDamageData newData2 = new EnemyRecievedDamageData(
                    attackedId,
                    new List<EnemyRecievedDamageSlot>() {
                    new EnemyRecievedDamageSlot(attackingId, damageAmount)
                       }
                    );

            _allDamageList.Add(newData2);
        }

        private void AddDamageSlot(EnemyRecievedDamageData data, string attackingId, float damageAmount)
        {
            foreach (EnemyRecievedDamageSlot slot in data.list)
            {
                if (slot.attackingId == attackingId)
                {
                    slot.accumulatedDamage += damageAmount;
                    return;
                }
            }

            data.list.Add(new EnemyRecievedDamageSlot(attackingId, damageAmount));

        }

        private void TimeTick()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                CountAgro();
            }
            else
            {
                if (_allDamageList == null)
                {
                    return;
                }

                if (_allDamageList.Count == 0)
                {
                    return;
                }

                GameRPCController.instance.SendLocalNoMasterDamageDataToMaster(JsonUtility.ToJson(new AllEnemiesRecievedDamageData(_allDamageList)));
                _allDamageList = null;
            }
        }

        private void CountAgro() // master only
        {
            if (_allDamageList == null)
            {
                return;
            }

            foreach (EnemyRecievedDamageData oneEnemyData in _allDamageList)
            {
                CountAgroForEnemy(oneEnemyData);
            }
        }

        private void CountAgroForEnemy(EnemyRecievedDamageData enemyData) // master only
        {
            if (enemyData.list == null)
            {
                return;
            }

            if (enemyData.list.Count == 0)
            {
                return;
            }

            string bestDamageCharacterId = string.Empty;
            float bestDamageResult = 0;


            foreach (EnemyRecievedDamageSlot slot in enemyData.list)
            {
                slot.accumulatedDamage *= 0.85f;

                if (slot.accumulatedDamage > bestDamageResult)
                {
                    bestDamageCharacterId = slot.attackingId;
                    bestDamageResult = slot.accumulatedDamage;
                }
            }

            if (enemyData.currentTargetId != bestDamageCharacterId)
            {
                ITarget enemy = TargetProvider.GetEnemyById(enemyData.enemyId);

                if (enemy != null)
                {
                    enemy.SetTarget(bestDamageCharacterId);
                }
            }
        }





        private void DamageInfoFromOtherPlayerRecieved(AllEnemiesRecievedDamageData allDamagedata)
        {
            Debug.Log("### 666 + " + JsonUtility.ToJson(allDamagedata));


            // statisctic !!!!

            


            foreach (EnemyRecievedDamageData data in allDamagedata.list)
            {
                float summDamage = 0;

                foreach (EnemyRecievedDamageSlot slot in data.list)
                {
                    DamageRecieved(slot.attackingId, data.enemyId, slot.accumulatedDamage);

                    summDamage += slot.accumulatedDamage;
                }


                TargetProvider.GetEnemyById(data.enemyId).GetStatsController().ReduceHealthFromRpc(summDamage);
            }




        }

        private void RemoveEnemyData(string enemyId, string killerId)
        {
            foreach (EnemyRecievedDamageData data in _allDamageList)
            {
                if (data.enemyId == enemyId)
                {
                    Debug.Log("#323# RemoveEnemyData " + enemyId);
                    _allDamageList.Remove(data);
                    break;
                }
            }
        }








        public void Dispose()
        {
            EventsProvider.OnEnemyRecievedDamage -= DamageRecieved;
            EventsProvider.EverySecond -= TimeTick;
            EventsProvider.OnEnemyDeathRpcRecieved -= RemoveEnemyData;


            EventsProvider.OnAllEnemiesRecievedDamageDataRpc -= DamageInfoFromOtherPlayerRecieved;
        }


    }





    [System.Serializable]
    public class AllEnemiesRecievedDamageData
    {
        public List<EnemyRecievedDamageData> list;

        public AllEnemiesRecievedDamageData(List<EnemyRecievedDamageData> list)
        {
            this.list = list;
        }
    }

    [System.Serializable]
    public class EnemyRecievedDamageData
    {
        public string enemyId;
        public string currentTargetId;
        public List<EnemyRecievedDamageSlot> list;

        public EnemyRecievedDamageData(string enemyId, List<EnemyRecievedDamageSlot> list)
        {
            this.enemyId = enemyId;
            this.list = list;
        }
    }


    [System.Serializable]
    public class EnemyRecievedDamageSlot
    {
        public string attackingId;
        public float accumulatedDamage;

        public EnemyRecievedDamageSlot(string allyId, float accumulatedDamage)
        {
            this.attackingId = allyId;
            this.accumulatedDamage = accumulatedDamage;
        }
    }
}