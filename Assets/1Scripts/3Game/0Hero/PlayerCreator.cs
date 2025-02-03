using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Game
{
    public class PlayerCreator : MonoBehaviour
    {

        [SerializeField] private LocalHero _localHeroPrefab;
        [SerializeField] private OtherHero _otherHeroPrefab;




        public LocalHero CreateLocalHero(HeroData heroData)
        {
            Debug.Log("# CreateLocalHero " + JsonUtility.ToJson(heroData));

            LocalHero localHero = Instantiate(_localHeroPrefab, Vector3.zero, Quaternion.identity);

            localHero.Setup(heroData);

            return localHero;
        }

        public OtherHero CreateOtherHero(PlayerData data)
        {
            Debug.Log("# CreateOtherHero " + JsonUtility.ToJson(data.nickname));

            OtherHero otherHero = Instantiate(_otherHeroPrefab, Vector3.zero, Quaternion.identity);

            otherHero.Setup(data);

            AiController.OnHeroPrefabCreated(otherHero, data);

            return otherHero;
        }


    }
}