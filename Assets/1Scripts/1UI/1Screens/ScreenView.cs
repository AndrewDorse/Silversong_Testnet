using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScreenView : MonoBehaviour
{
    public Enums.GameStage Type => _type;

    [SerializeField] private Enums.GameStage _type;
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _alpha;


    public virtual ScreenController Construct()
    {
        return new ScreenController(this);
    }

    public void DestroyScreenView()
    {
        Destroy(gameObject);
    }

    public void CloseScreen(Action callback = null)
    {
        StopAllCoroutines();

        if (!gameObject.activeInHierarchy) return;

        _alpha = 1;
        _canvasGroup.alpha = 1;

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        if (gameObject.activeInHierarchy) StartCoroutine(FadeCoroutine(Constants.SCREEN_DISEPPEARING_TIME, callback));
    }

    public void OpenScreen(Action callback = null)
    {
        StopAllCoroutines();

        _alpha = 0;
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        StartCoroutine(AppearCoroutine(Constants.SCREEN_APPEARING_TIME, callback));
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

        DestroyScreenView(); 
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
