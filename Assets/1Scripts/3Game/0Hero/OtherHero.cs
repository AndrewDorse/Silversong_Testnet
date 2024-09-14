using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace Silversong.Game
{
    public class OtherHero : MonoBehaviour, ITarget
    {
        [field: SerializeField] //todo remove
        public string UserId { get; private set; }
        public string Nickname { get; private set; }

        [SerializeField] private HeroData _heroData; // temp

        [SerializeField] private OtherHeroMovementController _movementController;
        [SerializeField] private HeroMesh _heroMesh;
        [SerializeField] private HeroAnimatorController _animatorController;

        public void Setup(HeroData heroData, string userId, string nickname)
        {
            _heroData = heroData;
            Nickname = nickname;
            UserId = userId;

            _heroMesh.SetClassAndRace(InfoProvider.instance.GetHeroClass(heroData.classId), InfoProvider.instance.GetSubrace(heroData.SubraceId));

            _heroMesh.TurnOffWeaponControllers();
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
            return null; // is it? TODO
        }


        public void SetTarget(string targetId)
        {

        }
        public string GetId()
        {
            return UserId;
        }

    }
}