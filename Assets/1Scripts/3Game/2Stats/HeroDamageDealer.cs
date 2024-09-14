using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDamageDealer
{
    

    public HeroDamageDealer()
    {
        EventsProvider.OnLocalHeroWeaponHitEnemyCollider += OnHitMelee;
    }

    private void OnHitMelee(ITarget target)
    {
        FightCalculation.CalculateDamage(HeroStatsController.instance.StatsController, 
            target.GetStatsController(), 
            PhotonNetwork.LocalPlayer.UserId);






    }

}
