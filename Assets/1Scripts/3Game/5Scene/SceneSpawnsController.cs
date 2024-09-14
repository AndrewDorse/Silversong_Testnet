using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Silversong.Game.Scene
{
    public class SceneSpawnsController : MonoBehaviour
    {
        [SerializeField] private SpawnsInfo _spawnInfo;


        private void Awake()
        {
            GameMaster.instance.SetSpawnPoints(_spawnInfo);
        }

    }


    [System.Serializable]
    public class SpawnsInfo
    {
        public BoxCollider[] _mainSpawnColliders;

        public BoxCollider[] _ambushSpawnColliders;

        public BoxCollider _bossSpawnCollider;
    }

}