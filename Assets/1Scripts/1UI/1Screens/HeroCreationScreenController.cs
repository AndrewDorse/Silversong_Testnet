using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCreationScreenController : ScreenController
{
    private readonly HeroCreationScreenView _view;

    private HeroData _heroData;

    private HeroClass _currentHeroClass;
    private Subrace _currentSubrace;

    public HeroCreationScreenController(HeroCreationScreenView view) : base(view)
    {
        _heroData = new HeroData();

        _view = view;
        _view.OpenScreen();

        _view.topButtons[0].onClick.AddListener(() => OpenPage(Enums.HeroCreationPage.heroClass));
        _view.topButtons[1].onClick.AddListener(() => OpenPage(Enums.HeroCreationPage.race));
        _view.topButtons[4].onClick.AddListener(() => OpenPage(Enums.HeroCreationPage.overview));

        _view.heroCreationOverviewPage.readyButton.onClick.AddListener(ReadyButtonClick);
         
        _currentSubrace = DataProvider.instance.GetSubrace(0); 

        SetHeroClasses(DataProvider.HeroClassProviderData.GetStartHeroClasses());
        SetRaces(DataProvider.instance.GetRaces());

        _view.heroRotator.SetupHeroObject(MenuComponents.instance.HeroObject);

    }


    public void OpenPage(Enums.HeroCreationPage pageType)
    {
        CloseAllPages();

        foreach(UIPage page in _view.uIPages)
        {
            if(page.pageType == pageType)
            {
                page.Open();
                break;
            }
        }
        
        if(pageType == Enums.HeroCreationPage.heroClass)
        {
            _view.classVariant.SetActive(true);
        }
        else if(pageType == Enums.HeroCreationPage.race)
        {
            _view.raceVariant.SetActive(true);
        }
        else if (pageType == Enums.HeroCreationPage.overview)
        {

        }
    }



    private void CloseAllPages()
    {
        _view.classVariant.SetActive(false);
        _view.raceVariant.SetActive(false);

        foreach (UIPage page in _view.uIPages)
        {
            page.Close();
        }
    }

    private void SetHeroClasses(HeroClass[] heroClasses)
    {

        for(int i = 0; i < _view.heroClassChoices.Length; i++ )
        {
            ClassSlotUI classSlot = _view.heroClassChoices[i];

            if(i < heroClasses.Length)
            {
                classSlot.gameObject.SetActive(true);
                classSlot.Setup(heroClasses[i], HeroClassSLotClick);
            }
            else
            {
                classSlot.gameObject.SetActive(false);
            }
        }


        HeroClassSLotClick(heroClasses[0]);
    }

    private void SetNextHeroClasses(HeroClass[] heroClasses)
    {
        for (int i = 0; i < _view.heroClassNextChoices.Length; i++)
        {
            ClassSlotUI classSlot = _view.heroClassNextChoices[i];

            if (i < heroClasses.Length)
            {
                classSlot.gameObject.SetActive(true);
                classSlot.Setup(heroClasses[i], HeroClassSLotClick);
            }
            else
            {
                classSlot.gameObject.SetActive(false);
            }

        }
    }


    private void SetRaces(Race[] races)
    {
        for (int i = 0; i < _view.raceSlots.Length; i++)
        {
            RaceSlotUI raceSlot = _view.raceSlots[i];

            if (i < races.Length)
            {
                raceSlot.gameObject.SetActive(true);
                raceSlot.Setup(races[i], RaceSlotClick);
            }
            else
            {
                raceSlot.gameObject.SetActive(false);
            }
        }

        RaceSlotClick(races[0], _view.raceSlots[0]);
    }

    private void SetSubraces(Subrace[] subraces)
    {
        for (int i = 0; i < _view.subraceSlots.Length; i++)
        {
            RaceSlotUI raceSlot = _view.subraceSlots[i];

            if (i < subraces.Length)
            {
                raceSlot.gameObject.SetActive(true);
                raceSlot.Setup(subraces[i], SubraceSlotClick);
            }
            else
            {
                raceSlot.gameObject.SetActive(false);
            }
        }
    }

    private void HeroClassSLotClick(HeroClass heroClass)
    {
        _view.heroInfoController.SetHeroClass(heroClass);

        SetNextHeroClasses(heroClass.nextClasses);

        _heroData.classId = heroClass.id;
        OnHeroDataUpdate();


        if(_currentHeroClass != heroClass)
        {
            _currentHeroClass = heroClass;

            EventsProvider.OnHeroClassSubraceChange(_currentHeroClass, _currentSubrace); // TODO can be changed to    OnHeroDataUpdate();
        } 
    }

    private void RaceSlotClick(Race race, RaceSlotUI slot)
    {
        ClearRacesSlots();
        SetSubraces(race.subraces);
        SubraceSlotClick(race.subraces[0], _view.subraceSlots[0]);
        slot.Choose(true);
    }

    private void SubraceSlotClick(Subrace subrace, RaceSlotUI slot)
    {
        ClearSubracesSlots();
        _view.heroInfoController.SetSubrace(subrace);
        slot.Choose(true);

        _heroData.SubraceId = subrace.id;
        OnHeroDataUpdate();

        if (_currentSubrace != subrace)
        {
            _currentSubrace = subrace;

            EventsProvider.OnHeroClassSubraceChange(_currentHeroClass, _currentSubrace);
        }
    }

    private void ClearSubracesSlots()
    {
        for (int i = 0; i < _view.subraceSlots.Length; i++)
        {
            _view.subraceSlots[i].Choose(false);
        }
    }

    private void ClearRacesSlots()
    {
        for (int i = 0; i < _view.raceSlots.Length; i++)
        {
            _view.raceSlots[i].Choose(false);
        }
    }

    private void OnHeroDataUpdate()
    {
        EventsProvider.OnHeroDataChanged?.Invoke(_heroData);
    }

    private void ReadyButtonClick()
    {
        Master.instance.ChangeGameStage(Enums.GameStage.inRoom);

    }
}
