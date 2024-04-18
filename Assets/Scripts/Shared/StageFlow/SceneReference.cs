using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicCombat.Shared.StageFlow
{
	[Serializable]
	[InlineProperty]
	internal struct SceneReference
	{
		[SerializeField]
		[HideInInspector]
		private int sceneIndex;
		
		public int SceneIndex => sceneIndex;
		
#if UNITY_EDITOR
		[SerializeField]
		[OnValueChanged(nameof(SetSceneIndex))]
		[HideLabel]
		[PropertyOrder(-1)]
		private SceneAsset sceneReference;
		
		[OnInspectorInit]
		private void SetSceneIndex()
		{
			if (!sceneReference) return;
			
			string scenePath = AssetDatabase.GetAssetPath(sceneReference);
			sceneIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
		}
#endif
	}
}