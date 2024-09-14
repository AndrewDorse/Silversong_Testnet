using Photon.Realtime;
using Silversong.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class EventsProvider
{

    public static Action<List<PlayerData>> OnPlayersDataChanged;

    public static Action OnJoinRoom;
    public static Action OnLeftRoom;

    public static Action<Player> OnOtherPlayerEnteredRoom;
    public static Action<Player> OnOtherPlayerLeftRoom;

    public static Action<NewUserIdDataRpc> OnOtherPlayerReconnect;

    public static Action<HeroData> OnHeroDataChanged;


    public static Action<PlayersDataRPC> OnGameDataRpcRecieved;
    public static Action OnBecomeMaster;




    public static Action OnGameStart;

    public static Action<Enums.GameStage> OnGameStateChange;




    // hero creation
    public static Action<HeroClass, Subrace> OnHeroClassSubraceChange;



    // GAME
    public static Action OnLevelStart;

    public static Action<HeroInfoRPC> OnOtherHeroInfoRpcRecieved;

    public static Action<Vector2> OnJoystickMove;

    public static Action<List<EnemyData>> OnEnemiesDataRpcRecieved;








    public static Action OnLevelEnd;


    // Battle

    public static Action<ITarget> OnLocalHeroWeaponHitEnemyCollider; // local hit
    public static Action<string, string> OnEnemyDeathRpcRecieved; // death rpc from master
    public static Action<string, string, float> OnEnemyRecievedDamage;// local damage to enemy
    public static Action<AllEnemiesRecievedDamageData> OnAllEnemiesRecievedDamageDataRpc; // damage info from other


    // local cast
    public static Action<int> OnAbilityButtonPressed; 
    public static Action<int> OnAbilityButtonReleased;
    public static Action OnAbilityCastStarted;
    public static Action<ActiveAbilitySlot> OnAbilityCastFinished;
    public static Action OnAbilityUseTrigger;
    //

    public static Action<BuffDataRPCSlot> OnBuffDataRpcRecieved;

    // local passives triggers
    public static Action<Enums.PassiveTrigger> OnLocalHeroPassiveTrigger;


    public static Action OnDeathOfAllEnemies;




    // inventory

    public static Action<List<ItemSlot>> OnInventoryItemsChanged;
    public static Action<ItemSlot> OnInventoryItemsAdded;






    // abilities
    public static Action<PassiveAbility> OnPassiveAbilityLearnt;



    //statistics
    public static Action<Silversong.Game.Statistics.LevelStatisticsData> OnStatisticsRpcRecieved;




    // Story choices
    public static Action<StoryChoiceRPCData> OnStoryChoiceRpcRecieved;
    public static Action<int> OnAllPlayersMadeChoice;




    // Timers
    public static Action TenTimesPerSecond;
    public static Action ThreeTimesPerSecond;

    public static Action EverySecond;

    public static Action EveryFiveSeconds;
}