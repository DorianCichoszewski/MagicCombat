using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace MagicCombat.Shared.StageFlow
{
	[Serializable]
	[InlineProperty]
	internal class SceneReference
	{
		[SerializeField]
		[HideLabel]
		private AssetReferenceScene sceneAssetReference;

		private AsyncOperationHandle<SceneInstance> loadedSceneHandle;

		public string SceneGUID => sceneAssetReference.AssetGUID;

		public bool HasScene => !string.IsNullOrWhiteSpace(sceneAssetReference.AssetGUID);

		public void LoadScene(Action callback, LoadSceneMode loadMode)
		{
			loadedSceneHandle = Addressables.LoadSceneAsync(sceneAssetReference, loadMode);
			loadedSceneHandle.Completed += _ => callback();
		}

		public void UnloadScene()
		{
			if (!loadedSceneHandle.IsDone)

				// No abort in addressable :(
				loadedSceneHandle.WaitForCompletion();

			Addressables.UnloadSceneAsync(loadedSceneHandle, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
		}

		[Serializable]
#if UNITY_EDITOR
		public class AssetReferenceScene : AssetReferenceT<SceneAsset>
#else
		public class AssetReferenceScene : AssetReferenceT<UnityEngine.Object>
#endif
		{
			public AssetReferenceScene(string guid) : base(guid) { }
		}
	}
}