using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.Game
{
    public class OtherHeroesController
    {
        //public List<OtherHero> OtherHeroes;



        public OtherHeroesController()
        {
            EventsProvider.OnLevelStart += OnLevelStart;
            EventsProvider.OnLevelEnd += OnLevelEnd;
            EventsProvider.OnOtherPlayerReconnect += UpdateOtherHeroOnReconnect;

        }


        private void OnLevelStart()
        {
            Subscribe();
        }

        private void OnLevelEnd()
        {
            Dispose();
        }

        private void Subscribe()
        {
            EventsProvider.OnOtherHeroInfoRpcRecieved += UpdateHero;
        }



        private void UpdateHero(HeroInfoRPC heroInfoRPC)
        {
            OtherHero otherHero = GetOtherHeroByUserId(heroInfoRPC.userId);

            otherHero.UpdatePosition(heroInfoRPC.position, heroInfoRPC.direction);
        }





        private OtherHero GetOtherHeroByUserId(string userId)
        {
            foreach (OtherHero otherHero in GameMaster.instance.OtherHeroes)
            {
                if (otherHero.UserId == userId)
                {
                    return otherHero;
                }
            }

            return null;
        }


        private void UpdateOtherHeroOnReconnect(NewUserIdDataRpc data)
        {
            OtherHero hero = GetOtherHeroByUserId(data.OldUserId);
            hero.SetNewUserId(data.UserId);
        }



        private void Dispose()
        {
            EventsProvider.OnOtherHeroInfoRpcRecieved -= UpdateHero;

        }

    }
}