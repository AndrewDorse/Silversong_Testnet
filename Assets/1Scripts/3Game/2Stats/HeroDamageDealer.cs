using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDamageDealer
{
    

    public HeroDamageDealer()
    {
        EventsProvider.OnLocalHeroWeaponHitEnemyCollider += OnHitMelee;
        EventsProvider.OnLocalAiHeroWeaponHitEnemyCollider += OnHitMeleeAi;
    }

    private void OnHitMelee(ITarget target)
    {
        FightCalculation.CalculateDamage(HeroStatsController.instance.StatsController, 
            target.GetStatsController(), 
            PhotonNetwork.LocalPlayer.UserId); 

    }

    private void OnHitMeleeAi(ITarget target, string id)
    {
        FightCalculation.CalculateDamage(AiController.GetPlayerStatsControllerById(id),
            target.GetStatsController(),
            id);

    }
}
