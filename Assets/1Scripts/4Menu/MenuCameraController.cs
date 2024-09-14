using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField] private CameraStageSlot[] _cameraSlots;

    private GameObject _currentCamera;


    private void Start()
    {
        EventsProvider.OnGameStateChange += MoveCamera;
    }

    private void MoveCamera(Enums.GameStage stage)
    {
        if(_currentCamera)
        {
            _currentCamera.SetActive(false);
        }

        foreach (CameraStageSlot slot in _cameraSlots)
        {
            if(slot.stage == stage)
            {
                _currentCamera = slot.camera; 
                _currentCamera.SetActive(true);
                break;
            }
        }
    }

    private void OnDestroy()
    {
        EventsProvider.OnGameStateChange -= MoveCamera;
    }

}

[System.Serializable]
public class CameraStageSlot
{
    public GameObject camera;
    public Enums.GameStage stage;
}
