using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.UI
{
    public class UIController : MonoBehaviour
    {

        private ScreenController _currentScreen;
        private ScreenView _currentView;


        [SerializeField] private PopupController _popupController;
        [SerializeField] private ScreenView[] _views;

        [SerializeField] private GameObject _transparentLoadingScreen;
        [SerializeField] private GameObject _loadingScreen;

        public void OpenScreen(Enums.GameStage type)
        {
            foreach(ScreenView view in _views)
            {
                if(view.Type == type)
                {
                    CreateView(view);
                }
            }
        }

        public void SetLightLoadingScreen()
        {
            _transparentLoadingScreen.SetActive(true);
        }

        public void SetLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void RemoveLoadingScreen()
        {
            StartCoroutine(RemoveLoadingScreenCoroutine());
        }

        private IEnumerator RemoveLoadingScreenCoroutine()
        {
            yield return new WaitForSeconds(0.15f);
            _transparentLoadingScreen.SetActive(false);
            _loadingScreen.SetActive(false);
        }

       






        private void CreateView(ScreenView view)
        {
            Clear();

            _currentView = Instantiate(view, transform);
            _currentScreen = _currentView.Construct();
        }

        private void Clear()
        {
            if(_currentScreen != null)
            {
                _currentScreen.Close();
            }
        }













        public Popup GetPopup(Enums.PopupType type)
        {
            return _popupController.GetPopup(type);
        }




    }
}