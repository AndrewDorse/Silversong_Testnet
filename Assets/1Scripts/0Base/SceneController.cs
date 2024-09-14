using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


namespace Silversong.Base
{
    public class SceneController
    {
        public void LoadScene(int sceneNumber, Action callback)
        {
            Master.instance.StartCoroutine(LoadSceneById(sceneNumber, callback));
        }

        public void LoadScene(string gameSceneName, Action callback)
        {
            Debug.Log("# LoadScene " + gameSceneName);

            Master.instance.StartCoroutine(LoadSceneByAddressablesName(gameSceneName, callback));
        }


        private IEnumerator LoadSceneById(int sceneNumber, Action callback)
        {
            Debug.Log("# LoadSceneById " + sceneNumber);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            callback?.Invoke();
        }

        private IEnumerator LoadSceneByAddressablesName(string sceneName, Action callback)
        {
            AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(sceneName);

            while (!sceneHandle.IsDone)
            {
                yield return null;
            }

            yield return new WaitForEndOfFrame();

            callback?.Invoke();
        }
    }
}