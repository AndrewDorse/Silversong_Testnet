using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Silversong.Game
{
    public class LocalHero : MonoBehaviour, ITarget
    {
        [SerializeField] private HeroData _heroData;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private HeroMesh _heroMesh;
        [SerializeField] private HeroAnimatorController _heroAnimatorController;

        private LocalHeroMovementController _movementController;




        public void Setup(HeroData heroData)
        {
            _heroData = heroData;

            _movementController = new LocalHeroMovementController(_characterController, _agent, _heroAnimatorController); // TODO change anim to Action<float> ???


            _heroMesh.SetClassAndRace(InfoProvider.instance.GetHeroClass(heroData.classId), InfoProvider.instance.GetSubrace(heroData.SubraceId));
            _heroAnimatorController.SetupForLocalHero();


        }



        public Vector3 GetPosition()
        {
            return transform.position;
        }





        private void OnDestroy()
        {
            _movementController.Dispose();

        }

        public StatsController GetStatsController()
        {
            return HeroStatsController.instance.StatsController;
        }

        public void SetTarget(string targetId)
        {
        }

        public string GetId()
        {
            return DataController.LocalPlayerData.userId;
        }
    }
}