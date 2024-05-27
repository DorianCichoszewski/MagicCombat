using Shared.Services;
using Shared.StageFlow;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Shared.GameState
{
	public class RuntimeInitializer
	{
		const string StartupScriptableLabel = "{StartupScriptable}";
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
		public static void RunStartupScriptables()
		{
			AssetLabelReference label = new AssetLabelReference
			{
				labelString = StartupScriptableLabel
			};
			var startups = Addressables.LoadAssetsAsync<StartupScriptable>(label, _ => { }).WaitForCompletion();

			StagesManager stagesManager = null;
			// Load ScriptableServiceLoader first and Stages Manager last
			for (int i = startups.Count - 1; i >= 0; i--)
			{
				var startup = startups[i];
				if (startup is ScriptableServiceLoader)
				{
					startup.GameStart();
					startups.RemoveAt(i);
				}
				else if (startup is StagesManager managerInList)
				{
					stagesManager = managerInList;
					startups.RemoveAt(i);
				}
			}

			foreach (var startup in startups)
			{
				ScriptableLocator.RegisterService(startup);
				startup.GameStart();
				
			}
			
			ScriptableLocator.RegisterService(stagesManager);
			stagesManager.GameStart();
		}
	}
}