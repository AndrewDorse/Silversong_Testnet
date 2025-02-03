using Silversong.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsScreenController : ScreenController
{
    private readonly StatsScreenView _view;
     


      
    private List<Enums.Stats> _baseStats = new List<Enums.Stats>() 
    { 
        Enums.Stats.hp, Enums.Stats.mp,
    Enums.Stats.strength, Enums.Stats.agility,Enums.Stats.intelligence,Enums.Stats.meleeDamage, Enums.Stats.rangeDamage,
    };

    private List<Enums.Stats> _attributes = new List<Enums.Stats>()
    { 
        Enums.Stats.strength, Enums.Stats.agility, Enums.Stats.intelligence,
        Enums.Stats.stamina, Enums.Stats.spirit, Enums.Stats.charisma, Enums.Stats.luck
    };

    private List<Enums.Stats> _attack = new List<Enums.Stats>()
    {
        Enums.Stats.meleeAttack, Enums.Stats.critChanceFinalPc, Enums.Stats.critDamageFinalPc, Enums.Stats.attackSpeedFinal,
        Enums.Stats.armorPenetrationFinal,
    };

    private List<Enums.Stats> _defense = new List<Enums.Stats>()
    {
        Enums.Stats.armor, Enums.Stats.hpRegeneration, Enums.Stats.defensePc, Enums.Stats.blockFinalPc,
        Enums.Stats.evadeFinalPc, Enums.Stats.parryFinalPc,
    };

    private List<Enums.Stats> _magic = new List<Enums.Stats>()
    {
        Enums.Stats.spellPower, Enums.Stats.spellDamage, Enums.Stats.spellHeal, 
        Enums.Stats.spellCritChanceFinalPc, Enums.Stats.spellCritDamageFinalPc, 
        Enums.Stats.spellLifestealPc, Enums.Stats.spellPenetrationFinalPc,
        Enums.Stats.spellSpeedFinal,
    };










    public StatsScreenController(StatsScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        SubcribeButtons();

        SetStatParameters(_baseStats);
        SetHeroData();

        if (DataController.instance.GameData.gameStage == Enums.ServerGameStage.camp)
        {
            _view.closeInPlaymodeButton.gameObject.SetActive(false);
            _view.botButtons[0].transform.parent.gameObject.SetActive(true);
        }
        else
        {
            _view.closeInPlaymodeButton.gameObject.SetActive(true);
            _view.botButtons[0].transform.parent.gameObject.SetActive(false);

            _view.closeInPlaymodeButton.onClick.AddListener(() => Master.instance.ChangeGameStage(Enums.GameStage.game));
        }
    }

    
    private void SetHeroData()
    {
        HeroClass heroClass = DataProvider.HeroClassProviderData.GetHeroClass(DataController.LocalPlayerData.heroData.classId);

        _view.portrait.sprite = heroClass.portrait;
        _view.className.text = heroClass.className;
        _view.level.text = "Level " + DataController.LocalPlayerData.heroData.level;

    }

    private void SetStatParameters(List<Enums.Stats> stats)
    {
        for(int i = 0; i < _view.statsParametersSlots.Length; i++)
        {
            if(i < stats.Count)
            {
                _view.statsParametersSlots[i].SetupStat(stats[i], HeroStatsController.instance.StatsController.GetStat(stats[i]));
            }
            else
            {
                _view.statsParametersSlots[i].gameObject.SetActive(false);
            } 
        }
    }
   
    
     


    























    private void SubcribeButtons()
    {
        _view.botButtons[0].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.camp));
        _view.botButtons[1].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.inventory));
        _view.botButtons[2].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.abilities));
        _view.botButtons[3].Setup(() => Master.instance.ChangeGameStage(Enums.GameStage.heroStats));

        _view.statsTypeButtons[0].onClick.AddListener(() => SetStatParameters(_baseStats));
        _view.statsTypeButtons[1].onClick.AddListener(() => SetStatParameters(_attributes));
        _view.statsTypeButtons[2].onClick.AddListener(() => SetStatParameters(_attack));
        _view.statsTypeButtons[3].onClick.AddListener(() => SetStatParameters(_defense));
        _view.statsTypeButtons[4].onClick.AddListener(() => SetStatParameters(_magic));


    }  

    public override void Dispose()
    {
        base.Dispose(); 
    }
}
