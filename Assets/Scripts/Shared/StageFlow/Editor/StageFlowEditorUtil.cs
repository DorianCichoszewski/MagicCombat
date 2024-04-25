using MagicCombat.Shared.GameState;
using UnityEngine.AddressableAssets;

#if UNITY_EDITOR
namespace MagicCombat.Shared.StageFlow.Editor
{
	internal class StageFlowEditorUtil
	{
		private static StageFlowEditorUtil instance;

		public static StageFlowEditorUtil Instance => instance ??= new();

		public StageFlowWindow window;
		private StagesManager stagesManager;

		public StageOrderedList EditorList
		{
			get
			{
				if (stagesManager == null)
				{
					Addressables.LoadAssetsAsync<SharedScriptable>("{StartupScriptable}", candidate =>
					{
						if (candidate == null) return;
						stagesManager = candidate.StagesManager;
					}).WaitForCompletion();
				}

				stagesManager.SetupStages();
				return stagesManager.Stages;
			}
		}

		public void Refresh()
		{
			window?.Refresh();
		}
	}
}
#endif