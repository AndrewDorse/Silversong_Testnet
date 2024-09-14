using Silversong.Game;
using Silversong.Game.Statistics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsScreenController : ScreenController
{
    private readonly StatisticsScreenView _view;

    public StatisticsScreenController(StatisticsScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        _view.continueButton.onClick.AddListener(Continue);


        SetLevelStatisticsInfo(GameMaster.instance.GetLevelStatistics());
    }


    private void SetLevelStatisticsInfo(List<PlayerStatisticsSlot> data)
    {
        for (int i = 0; i < _view.slots.Length; i++)
        {
            StatisticsSlotUI slotUI = _view.slots[i];

            if(i < data.Count)
            {
                slotUI.gameObject.SetActive(true);

                slotUI.Setup(data[i]);               
            }
            else
            {
                slotUI.gameObject.SetActive(false);
            }




        }
    }



    private void Continue()
    {
        // rewards ??? or camp  

        Master.instance.ChangeGameStage(Enums.GameStage.rewards);
    }



}
