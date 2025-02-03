using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace Silversong.Game
{
    public class OtherHero : MonoBehaviour, ITarget
    { 
        [field: SerializeField] public string UserId { get; private set; }
        [field: SerializeField] public string Nickname { get; private set; }

        [SerializeField] private HeroData _heroData; // temp

        [SerializeField] private OtherHeroMovementController _movementController;
        [SerializeField] private HeroMesh _heroMesh;
        [SerializeField] private HeroAnimatorController _animatorController;
        [SerializeField] private HealthBar _healthBar;


        private AiPlayerController _aiPlayerController;


        public void Setup(PlayerData data)
        {
            _heroData = data.heroData;
            Nickname = data.nickname;
            UserId = data.userId;

            _heroMesh.SetClassAndRace(DataProvider.HeroClassProviderData.GetHeroClass(data.heroData.classId),
                DataProvider.instance.GetSubrace(data.heroData.SubraceId));

            if (data.ai == false)
            {
                _heroMesh.TurnOffWeaponControllers();
            }
            else
            {
                if(PhotonNetwork.IsMasterClient)
                { 
                    _heroMesh.SetupWeaponControllers(data.userId);
                    _aiPlayerController = new AiPlayerController(_movementController);
                    _movementController.enabled = false;

                    AiController.GetPlayerStatsControllerById(data.userId).UpdateTransform(transform);
                }
            }



            EventsProvider.OnOtherHeroInfoRpcRecieved += UpdatePlayerInfo;

        }

        public void UpdatePosition(Vector3 position, Vector3 direction)
        {
            _movementController.UpdatePosition(position, direction);
        }

        public void SetNewUserId(string userId)
        {
            UserId = userId;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public StatsController GetStatsController()
        {
            if(_aiPlayerController != null)
            {
                return AiController.GetPlayerStatsControllerById(UserId);
            }

            return null;
        }


        public void SetTarget(string targetId)
        {

        }

        public string GetId()
        {
            return UserId;
        }

        public void OnHpReduced(float value, string attackingId)
        {
            if(value <= 0)
            {
                DeathLocal(attackingId);
            }

            _healthBar.SetValueLocal(value);
        }




        private void UpdatePlayerInfo(HeroInfoRPC data)
        { 
            if (UserId == data.userId)
            {
                _healthBar.SetValueFromRpc(data.healthPc);
            } 
        }







        private void DeathLocal(string killerId)
        {
            if (PhotonNetwork.IsMasterClient == true)
            {
                // Dispose();
                 gameObject.SetActive(false);

                if (_aiPlayerController != null)
                {
                    _aiPlayerController.Dispose();
                }
            }
        }

        public bool CanBeAttacked()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (_aiPlayerController != null)
                {
                    StatsController statsController = AiController.GetPlayerStatsControllerById(UserId);

                    if (statsController == null)
                    {
                        return false;
                    }

                    if (statsController.CurrentHp <= 0)
                    {
                        return false;
                    }
                }
            } 

            return true;
        }

        private void OnDestroy()
        {
            EventsProvider.OnOtherHeroInfoRpcRecieved -= UpdatePlayerInfo;


            if (_aiPlayerController != null)
            {
                _aiPlayerController.Dispose();
            }
        }

        public bool IsAi()
        {
           return _aiPlayerController != null;
        }
    }
}