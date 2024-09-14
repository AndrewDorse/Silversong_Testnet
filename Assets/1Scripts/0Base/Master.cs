using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silversong.Base;
using UnityEngine.AddressableAssets;
using Silversong.UI;

public class Master : MonoBehaviour
{
    public static Master instance;

    private GameStageController _gameStageController = new GameStageController();
    private SilversongConnector _silversongConnector = new SilversongConnector();
    private GameStarter _gameStarter = new GameStarter();
    private AddressablesDownloader _addressablesDownloader = new AddressablesDownloader();
    private GameLevelsGenerator _storyController = new GameLevelsGenerator();

    [SerializeField] private UIController _uIController;
    [SerializeField] private PhotonConnector _photonConnector;
    [SerializeField] private AssetLabelReference _assetLabel;









    private void Awake() => instance = this;

    private void Start()
    {
        //StartCoroutine(_addressablesDownloader.Load(_assetLabel));
        _gameStarter.Initialize();
    }

    public void ChangeGameStage(Enums.GameStage stage)
    {
        if(stage == Enums.GameStage.game)
        {
            _gameStageController.LaunchGameStage(DataController.instance.GameData.sceneName);
            return;
        }

        _gameStageController.ChangeStage(stage);
    }

    public void OpenScreen(Enums.GameStage stage)
    {
        _uIController.OpenScreen(stage);
    }

    public Popup GetPopup(Enums.PopupType type)
    {
        return _uIController.GetPopup(type);
    }







    // connection
    public void ConnectToPhoton()
    {
        SetLightLoadingScreen();
        _photonConnector.Connect();
    }

    public void StartQuickGame()
    {
        _photonConnector.StartQuickGame();
    }

    public void OnPhotonConnection()
    {
        _silversongConnector.OnPhotonConnection();
    }

    public void OnSuccessfullLogin()
    {
        RemoveLoadingScreen();

        ChangeGameStage(Enums.GameStage.menu);
    }

    public void LeaveRoom()
    {
        _photonConnector.LeaveRoom();
        DataController.instance.LocalData.RoomId = string.Empty;
        SaveController.SaveLocalData();
    }








    // TODO maybe move it somewhere????? !!

    // loading screen
    public void SetLightLoadingScreen()
    {
        _uIController.SetLightLoadingScreen();
    }

    public void SetLoadingScreen()
    {
        _uIController.SetLoadingScreen();
    }

    public void RemoveLoadingScreen()
    {
        _uIController.RemoveLoadingScreen();
    }

}
