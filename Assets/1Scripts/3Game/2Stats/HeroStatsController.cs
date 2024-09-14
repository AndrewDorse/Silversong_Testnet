using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatsController : MonoBehaviour
{
    public static HeroStatsController instance;

    

    public StatsController StatsController { get => _statsController; private set => _statsController = value; }

    private StatsController _statsController;
    private HeroDamageDealer _heroDamageDealer;
    private LocalHeroAbilityCaster _abilityCaster;



    private void Awake()
    {
        instance = this;

        EventsProvider.OnGameStart += Initialize;
        EventsProvider.OnLevelEnd += OnLevelEnd;
    }

    private void Initialize()
    {
        HeroData heroData = DataController.instance.GetMyHeroData();

        _heroDamageDealer = new HeroDamageDealer();

        _statsController = new StatsController(heroData);


        _abilityCaster = new LocalHeroAbilityCaster();
    }


    private void OnLevelEnd()
    {
        if(_statsController.CurrentHp > 0)
        {
           
        }

        LevelUp();
    }

    private void LevelUp()
    {
        DataController.LocalPlayerData.heroData.level++;
        DataController.LocalPlayerData.heroData.TalentPoints++;

        Debug.Log("#LevelUp# " + DataController.LocalPlayerData.heroData.level);
    }







}
