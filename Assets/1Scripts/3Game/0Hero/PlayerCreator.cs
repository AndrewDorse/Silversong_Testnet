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

        public OtherHero CreateOtherHero(HeroData heroData, string userId, string nickname)
        {
            Debug.Log("# CreateOtherHero " + JsonUtility.ToJson(heroData));

            OtherHero otherHero = Instantiate(_otherHeroPrefab, Vector3.zero, Quaternion.identity);

            otherHero.Setup(heroData, userId, nickname);

            return otherHero;
        }


    }
}