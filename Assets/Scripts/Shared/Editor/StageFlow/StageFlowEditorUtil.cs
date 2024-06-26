using Shared.GameState;
using Shared.StageFlow;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

#if UNITY_EDITOR
namespace Shared.Editor.StageFlow
{
	internal class StageFlowEditorUtil
	{
		private static StageFlowEditorUtil instance;

		public static StageFlowEditorUtil Instance => instance ??= new StageFlowEditorUtil();

		public StageFlowWindow window;
		private StagesManager stagesManager;

		public StageOrderedList EditorList
		{
			get
			{
				if (stagesManager == null)
					Addressables.LoadAssetsAsync<StartupScriptable>("{StartupScriptable}", candidate =>
					{
						if (candidate is StagesManager manager)
						{
							stagesManager = manager;
						}
					}).WaitForCompletion();

				stagesManager?.SetupStages();
				return stagesManager?.Stages;
			}
		}

		public void RunStage(StageData stage)
		{
			PlayerPrefs.SetString(StagesManager.PlayerPrefsKey, stage.SceneReference.SceneGUID);
			EditorApplication.EnterPlaymode();
		}

		public void Refresh()
		{
			window?.Refresh();
		}
	}
}
#endif