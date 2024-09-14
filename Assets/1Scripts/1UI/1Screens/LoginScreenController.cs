using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScreenController : ScreenController
{
    private LoginScreenView _view;

    

    public LoginScreenController(LoginScreenView view) : base(view)
    {
        _view = view;
        _view.OpenScreen();

        _view.loginButton.onClick.AddListener(Login);
        _view.serverButton.onClick.AddListener(OpenServerList);
        _view.closeServerButton.onClick.AddListener(CloseServerList);
    }

    private void Login()
    {
        Master.instance.ConnectToPhoton();
    }

    private void OpenServerList()
    {
        _view.serversWindow.SetActive(true);
        SetList(PhotonNetwork.NetworkingClient.RegionHandler.EnabledRegions);

        EventsProvider.EverySecond += UpdateServerList;
    }

    private void CloseServerList()
    {
        _view.serversWindow.SetActive(false);

        EventsProvider.EverySecond -= UpdateServerList;
    }

    private void UpdateServerList()
    {
        PhotonNetwork.NetworkingClient.RegionHandler.PingMinimumOfRegions(OnResultsRecieved,
            PhotonNetwork.NetworkingClient.RegionHandler.SummaryToCache);

    }

    private void OnResultsRecieved(RegionHandler regionHandler)
    {
        SetList(regionHandler.EnabledRegions);
    }

    private void SetList(List<Region> list)
    {
        for (int i = 0; i < _view.serverSlots.Length; i++)
        {
            if (i < list.Count)
            {
                _view.serverSlots[i].gameObject.SetActive(true);

                if (list[i].Code == "asia") // Lobby.instance.GetRegion().Code)
                {
                    _view.serverSlots[i].SetServerInfo(list[i], true, ChooseRegion);
                }
                else
                {
                    _view.serverSlots[i].SetServerInfo(list[i], false, ChooseRegion);
                }
            }
            else
            {
                _view.serverSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void ChooseRegion(Region region)
    {
        Debug.Log(" ## #ChooseRegion +  " + region.Ping + "  " + region.Code);

        DataController.instance.LocalData.regionCode = region.Code;
        SaveController.SaveLocalData(); // ???

        CloseServerList();
    }


}
