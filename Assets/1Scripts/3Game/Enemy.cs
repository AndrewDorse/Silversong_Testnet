using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;



namespace Silversong.Game
{
    public class Enemy : MonoBehaviour, ITarget
    {
        [field: SerializeField] public string Id { get; private set; }

        [field: SerializeField] public string TargetId { get; private set; }
        private ITarget _target;


        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private EnemyMovementController _movementController;
         

        private StatsController _stats;
        private EnemyModelController _modelController;
        private EnemyAnimatorController _animatorController;


        public void Setup(EnemyData enemyData, EnemyModelController enemyModelController)
        {
            Id = enemyData.id;

            _stats = new StatsController(enemyData, OnHpReduced, OnControlStatesChanged, transform);


            _modelController = enemyModelController;

            _animatorController = new EnemyAnimatorController(_modelController.Animator);
            _movementController.Setup(_animatorController);

            EventsProvider.TenTimesPerSecond += Tick;
        }



        public void UpdateInfo(EnemyData enemyData)
        {
            if (PhotonNetwork.IsMasterClient == false)
            {
                _movementController.UpdatePosition(enemyData.position);

                TargetId = enemyData.targetId;
                _target = TargetProvider.GetAllyTargetByTargetId(TargetId).Item1;


                _movementController.Target = _target;


                _stats.CurrentHpInfoFromMaster(enemyData.health);
                _healthBar.SetValueFromRpc(enemyData.healthPc);
            }
        }

        public void SetTarget(string targetId)
        {
            TargetId = targetId;
            _target = TargetProvider.GetAllyTargetByTargetId(targetId).Item1;
            _movementController.Target = _target;
        }

        public void Hit()
        { 
            if(_target != null)
            {
                if(_target.GetId() == DataController.instance.LocalData.UserId)
                { 
                    FightCalculation.CalculateDamage(_stats, _target.GetStatsController(), TargetId);
                }

                if (PhotonNetwork.IsMasterClient == true)
                {
                    if (AiController.TargetIsAi(_target.GetId()))
                    {
                        FightCalculation.CalculateDamage(_stats, _target.GetStatsController(), TargetId);
                    }
                }
            }
        }





        private void Tick()
        {
            if (TargetId == string.Empty)
            { 
                (_target, TargetId) = TargetProvider.GetClosestAlly(transform.position);

                if (_target == null)
                {
                    return;
                } 
            }

            if (_target.CanBeAttacked() == false)
            {
                (_target, TargetId) = TargetProvider.GetClosestAlly(transform.position);

                if (_target == null)
                {
                    return;
                }
            } 

            _movementController.Target = _target;
        }

        private void OnHpReduced(string attackingId, float value, bool fromRpc)
        {
            if (!fromRpc)
            {
                EventsProvider.OnEnemyRecievedDamage?.Invoke(attackingId, Id, value);
            }

            _healthBar.SetValueLocal(_stats.CurrentHp / _stats.GetStat(Enums.Stats.hp)); // change it to coroutine? to change hbar smoothly??? TODO check

            if (TargetId == string.Empty)
            {
                (_target, TargetId) = TargetProvider.GetClosestAlly(transform.position);

                if (_target == null)
                {
                    return;
                }

                _movementController.Target = _target;
            }

            // damage effect
            TMP_Text text = ObjectsPool.Spawn(DataProvider.BattlePrefabsProviderData.CombatText, transform.position + Vector3.up * 1f, Quaternion.identity);
            text.text = "-" + Mathf.RoundToInt(value).ToString();

            int random = UnityEngine.Random.Range(0, 100);

            if (random > 50)
            {
                GameObject bloodEffect = ObjectsPool.Spawn(DataProvider.BattlePrefabsProviderData.Effects[0], transform.position + Vector3.up * 1f, Quaternion.identity);
            }

            GameObject hitEffect = ObjectsPool.Spawn(DataProvider.BattlePrefabsProviderData.Effects[1], transform.position + Vector3.up * 1f, Quaternion.identity);

            // passives here too??


            if (_stats.CurrentHp <= 0)
            {
                DeathLocal(attackingId);
            }
        }













        public void DeathFromMaster()
        {
            Dispose();
            Destroy(gameObject);
        }


        private void DeathLocal(string killerId)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                GameRPCController.instance.SendEnemyDeathToOthers(Id, killerId);
                EventsProvider.OnEnemyDeathRpcRecieved?.Invoke(Id, killerId);
            }
        }


        private void Dispose()
        {


            //_stats.dispose????/


            EventsProvider.TenTimesPerSecond -= Tick;
            _animatorController.Dispose();
        }


        private void OnDestroy()
        {
            Dispose();
        }






        private void OnControlStatesChanged(Enums.ControlState stateToShow, bool canMove, bool canCast, bool canAttack, bool canRotate)
        {
            _movementController.CanMove = canMove;
            _movementController.CanRotate = canRotate;

            _animatorController.SetControlState(stateToShow);

        }






        // interface
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public StatsController GetStatsController()
        {
            return _stats;
        }

        public string GetId()
        {
            return Id;
        }

        public bool CanBeAttacked()
        {
            return true;
        }

        public bool IsAi()
        {
            return false;
        }
    }
}