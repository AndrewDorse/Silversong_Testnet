using Silversong.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScreenController : ScreenController
{
    private readonly AbilitiesScreenView _view;
    private Enums.AbilityType _state = Enums.AbilityType.Class;

    private int _currentId;
    private bool _activeAbilities = true;


    public AbilitiesScreenController(AbilitiesScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        SubcribeButtons();

        OpenClassAbilities();
        


        SetupMagicSchoolsButtons();

        _view.activePassiveToggle.onClick.AddListener(ActivePassiveToggle);
    }

    private void ActivePassiveToggle()
    {
        _activeAbilities = !_activeAbilities;


        if(_state == Enums.AbilityType.Class)
        {
            OpenClassAbilities();
        }
        else if (_state == Enums.AbilityType.Common)
        {
            OpenCommonAbilities();
        }
        else if (_state == Enums.AbilityType.Witchcraft)
        {
            OpenWitchcraftAbilities();
        }
    }



    private void Setup(List<ActiveAbility> abilities)
    {
        for (int i = 0; i < _view.abilitySlots.Length; i++)
        {
            if (i < abilities.Count)
            {
                _view.abilitySlots[i].gameObject.SetActive(true);
                _view.abilitySlots[i].Setup(abilities[i], AbilityClick);
            }
            else
            {
                _view.abilitySlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void Setup(List<PassiveAbility> abilities)
    {
        for (int i = 0; i < _view.abilitySlots.Length; i++)
        {
            if (i < abilities.Count)
            {
                _view.abilitySlots[i].gameObject.SetActive(true);
                _view.abilitySlots[i].Setup(abilities[i], AbilityClick);
            }
            else
            {
                _view.abilitySlots[i].gameObject.SetActive(false);
            }
        }
    }



    private void AbilityClick(int id)
    {
        AbilityPopup popup = Master.instance.GetPopup(Enums.PopupType.ability) as AbilityPopup;

        if (_activeAbilities)
        {
            ActiveAbility ability = InfoProvider.instance.GetAbility(id);


            (Enums.UniversalButtonType, Action, int) openInfo = GetAbilityOpenType(ability);

            popup.Setup(ability, openInfo.Item1, openInfo.Item2, openInfo.Item3);
        }
        else
        {

            PassiveAbility ability = InfoProvider.instance.GetPassive(id);
            (Enums.UniversalButtonType, Action, int) openInfo = GetAbilityOpenType(ability);
            popup.Setup(ability, openInfo.Item1, openInfo.Item2, openInfo.Item3);



        }

        _currentId = id;
    }

    private (Enums.UniversalButtonType, Action, int) GetAbilityOpenType(ActiveAbility ability) // to separate class !!! 
    {
        int levelAbility = DataController.instance.GetActiveAbilityLevel(ability.Id);
        int value = 1;
        Enums.UniversalButtonType result = Enums.UniversalButtonType.canLearn;
        Action callback = null;


        if(levelAbility == ability.MaxLevel)
        {
            value = 0;
            result = Enums.UniversalButtonType.maxLevel;
        }
        else if (DataController.instance.IsEnoughPointsToLearn(ability.LevelInfo[levelAbility].RequiredPoints))
        {
            if (levelAbility == 0)
            {
                result = Enums.UniversalButtonType.canLearn;                
                callback = LearnAbility;
            }
            else
            {
                result = Enums.UniversalButtonType.CanUpgrade;
                callback = UpgradeAbility;
            }

            value = ability.LevelInfo[levelAbility].RequiredPoints;
        }
        else if (!DataController.instance.IsEnoughPointsToLearn(ability.LevelInfo[levelAbility].RequiredPoints))
        {
            if (levelAbility == 0)
            {
                result = Enums.UniversalButtonType.notEnoughPointsToLearn;
            }
            else
            {
                result = Enums.UniversalButtonType.notEnoughPointsToUpgrade;
            }
        }
        else if (!DataController.instance.IsEnoughSlotsToLearn())
        {
            result = Enums.UniversalButtonType.maxAmount;
        }
        


        return (result, callback, value);
    }
    private (Enums.UniversalButtonType, Action, int) GetAbilityOpenType(PassiveAbility ability) // to separate class !!! 
    {
        int levelAbility = DataController.instance.GetActiveAbilityLevel(ability.Id);
        int value = 1;
        Enums.UniversalButtonType result = Enums.UniversalButtonType.canLearn;
        Action callback = null;


        if (levelAbility == ability.MaxLevel)
        {
            value = 0;
            result = Enums.UniversalButtonType.maxLevel;
        }
        else if (DataController.instance.IsEnoughPointsToLearn(ability.LevelInfo[levelAbility].RequiredPoints))
        {
            if (levelAbility == 0)
            {
                result = Enums.UniversalButtonType.canLearn;
                callback = LearnAbility;
            }
            else
            {
                result = Enums.UniversalButtonType.CanUpgrade;
                callback = UpgradeAbility;
            }

            value = ability.LevelInfo[levelAbility].RequiredPoints;
        }
        else if (!DataController.instance.IsEnoughPointsToLearn(ability.LevelInfo[levelAbility].RequiredPoints))
        {
            if (levelAbility == 0)
            {
                result = Enums.UniversalButtonType.notEnoughPointsToLearn;
            }
            else
            {
                result = Enums.UniversalButtonType.notEnoughPointsToUpgrade;
            }
        }
        



        return (result, callback, value);
    }



    private void LearnAbility()
    {
        if (_activeAbilities)
        {
            DataController.instance.AddActiveAbility(_currentId);
        }
        else
        {
            DataController.instance.AddPassiveAbility(_currentId);
        }
    }

    private void UpgradeAbility()
    {
        if (_activeAbilities)
        {
            DataController.instance.AddActiveAbility(_currentId);
        }
        else
        {
            DataController.instance.AddPassiveAbility(_currentId);
        }
    }














        private void SetupMagicSchoolsButtons()
    {
        for(int i = 0; i < _view.magicSchoolButtons.Length; i++)
        {
            _view.magicSchoolButtons[i].Setup(i, ClickMagicSchool);

            if(i == 0)
            {
                _view.magicSchoolButtons[i].Chosen(true);
            }
        }
    }










    private void ClickMagicSchool(int id)
    {
        for (int i = 0; i < _view.magicSchoolButtons.Length; i++)
        {
            if (id == i)
            {
                _view.magicSchoolButtons[i].Chosen(true);
            }
            else
            {
                _view.magicSchoolButtons[i].Chosen(false);

            }

        }
        Debug.Log("#OpenSchool# " + (Enums.MagicSchools)id + 1); // coz 0 is none

    }









    private void OpenClassAbilities() 
    {
        ClearAbilityGroupsView(0);
        _view.magicSchoolsPanel.SetActive(false);
        _state = Enums.AbilityType.Class;


        if (_activeAbilities)
        {
            Setup(InfoProvider.instance.GetHeroClass(DataController.LocalPlayerData.heroData.classId).ActiveAbilities);
        }
        else
        {
            Setup(InfoProvider.instance.GetHeroClass(DataController.LocalPlayerData.heroData.classId).PassiveAbilities);
        }
    }

    private void OpenCommonAbilities()  
    {
        ClearAbilityGroupsView(1);
        _view.magicSchoolsPanel.SetActive(false);
        _state = Enums.AbilityType.Common;

        if (_activeAbilities)
        {
            Setup(InfoProvider.instance.GetCommonSpells());
        }
        else
        {
            Setup(InfoProvider.instance.GetCommonPassives());
        }
    }


    private void OpenWitchcraftAbilities()
    {
        ClearAbilityGroupsView(2);
        _view.magicSchoolsPanel.SetActive(true);
        _state = Enums.AbilityType.Witchcraft;

        if (_activeAbilities)
        {
            Setup(InfoProvider.instance.GetSpellsBySchool(Enums.MagicSchools.fire));
        }
        else
        {
            Setup(InfoProvider.instance.GetPassivesBySchool(Enums.MagicSchools.fire));
        }
    }

    private void ClearAbilityGroupsView(int number)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == number)
            {
                _view.abilityGroupsButtons[i].Chosen(true);
            }
            else
            {
                _view.abilityGroupsButtons[i].Chosen(false);
            }
        }
    }




















    private void SubcribeButtons()
    {
        _view.botButtons[0].Setup(OpenCamp);
        _view.botButtons[1].Setup(OpenInventory); 
        _view.botButtons[2].Setup(OpenAbilities);


        _view.abilityGroupsButtons[0].Setup(OpenClassAbilities);
        _view.abilityGroupsButtons[1].Setup(OpenCommonAbilities);
        _view.abilityGroupsButtons[2].Setup(OpenWitchcraftAbilities);

    }


    private void OpenCamp()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.camp);
    }

    private void OpenInventory()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.inventory);
    }

    private void OpenAbilities()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.abilities);
    }

    
}
