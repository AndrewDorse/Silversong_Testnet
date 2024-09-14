using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Silversong.UI
{
    public class Popup : MonoBehaviour
    { 
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] protected Button _backBGButton;


        private float _alpha;


        public void Initialize(Action backCallback)
        {
            _backBGButton.onClick.AddListener(() => backCallback?.Invoke());
        }



        public void Close(Action callback = null)
        {
            StopAllCoroutines();

            if (!gameObject.activeInHierarchy) return;

            _alpha = 1;
            _canvasGroup.alpha = 1;

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            if (gameObject.activeInHierarchy) StartCoroutine(FadeCoroutine(Constants.SCREEN_DISEPPEARING_TIME, callback));
        }

        public void Open(Action callback = null)
        {
            StopAllCoroutines();

            _alpha = 0;
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            StartCoroutine(AppearCoroutine(Constants.SCREEN_APPEARING_TIME, callback));
        } 

        public void Destroy()
        {
            Destroy(gameObject);
        }
          

        private IEnumerator FadeCoroutine(float time, Action callback = null)
        {
            while (_alpha > 0)
            {
                _alpha -= Time.deltaTime / time;
                _canvasGroup.alpha = _alpha;

                yield return null;
            }

            callback?.Invoke();

            Destroy();
        }

        private IEnumerator AppearCoroutine(float time, Action callback = null)
        {
            while (_alpha < 1)
            {
                _alpha += Time.deltaTime / time;
                _canvasGroup.alpha = _alpha;

                yield return null;
            }

            callback?.Invoke();
        } 
    }
}