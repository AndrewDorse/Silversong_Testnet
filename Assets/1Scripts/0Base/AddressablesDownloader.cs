using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class AddressablesDownloader
{



	public IEnumerator Load(AssetLabelReference assetLabel)
	{

		AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(assetLabel);
		yield return getDownloadSize;

		Debug.Log("### 214 getDownloadSize = " + getDownloadSize.Result);

		//If the download size is greater than 0, download all the dependencies.
		if (getDownloadSize.Result > 0)

		{ 
		
			AsyncOperationHandle downloadDependencies = Addressables.DownloadDependenciesAsync(assetLabel);

			downloadDependencies.Completed += OnLoadedDependincies;

			yield return downloadDependencies;
		}
	}



		private void OnLoadedDependincies(AsyncOperationHandle handle)
	{
		Debug.Log("### 214 OnLoadedDependincies is "  + handle.IsDone + " status " + handle.Status);

		

		AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync("1Forest", UnityEngine.SceneManagement.LoadSceneMode.Additive, false);
		//sceneHandle.Completed += OnLoadedScene;

	}
	private void OnLoadedScene(AsyncOperationHandle<SceneInstance> handle)
	{
		//Debug.Log("### 214 OnLoadedScene " + handle.Result);
		//handle.Result.ActivateAsync();
	}
}
