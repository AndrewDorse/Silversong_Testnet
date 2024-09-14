using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
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

            _stats = new StatsController(enemyData, OnHpReduced, OnControlStatesChanged);


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







        private void Tick()
        {
            if (TargetId == string.Empty)
            {
                (_target, TargetId) = TargetProvider.GetClosestAlly(this);

                if (_target == null)
                {
                    return;
                }

                _movementController.Target = _target;
            }
        }

        private void OnHpReduced(string attackingId, float value, bool fromRpc)
        {
            if (!fromRpc)
            {
                EventsProvider.OnEnemyRecievedDamage?.Invoke(attackingId, Id, value);
            }

            // passives here too??

            _healthBar.SetValueLocal(_stats.CurrentHp / 100f); // change it to coroutine? to change hbar smoothly??? TODO check


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
    }
}