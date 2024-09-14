using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Silversong.UI
{
    public class PopupController : MonoBehaviour
    {

        [SerializeField] private PopupSlotData[] _popups;

        private List<Popup> _popupLayouts;


        private void Start()
        {
            if (_popupLayouts == null)
            {
                _popupLayouts = new List<Popup>();
            }
        }

        public Popup GetPopup(Enums.PopupType type) // add slots for diff types of popups
        {
            Popup itemPopup = Instantiate(GetPopupByType(type), transform);

            itemPopup.Initialize(Back);

            _popupLayouts.Add(itemPopup);

            return itemPopup;
        }


        private Popup GetPopupByType(Enums.PopupType type)
        {
            foreach(PopupSlotData popup in _popups)
            {
                if (popup.PopupType == type)
                {
                    return popup.Popup;
                }
            }

            return null;
        }








        private void Back()
        {
            if (_popupLayouts == null) return;
            if (_popupLayouts.Count == 0) return;

            _popupLayouts[_popupLayouts.Count - 1].Close(_popupLayouts[_popupLayouts.Count - 1].Destroy);

            _popupLayouts.Remove(_popupLayouts[_popupLayouts.Count - 1]);
        }




    }



    [System.Serializable]
    public class PopupSlotData
    {
        public Popup Popup;
        public Enums.PopupType PopupType;

    }
}