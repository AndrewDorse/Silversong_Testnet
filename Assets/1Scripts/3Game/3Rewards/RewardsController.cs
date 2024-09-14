using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsController 
{
    
    public RewardSlot[] RewardSlots { get; private set; }




    public RewardsController()
    {

        EventsProvider.OnEnemyDeathRpcRecieved += OnEnemyDeath;
    }


    public void SetStoryOptionRewards(RewardSlot[] rewards)
    {
        RewardSlots = rewards;

    }



    private void OnEnemyDeath(string enemyId, string killerId)
    {
        // need to know mobId too, for money reward


        if(DataController.LocalPlayerData.userId == killerId)
        {
            DataController.LocalPlayerData.heroData.Gold += 1;


        }
    }


}
