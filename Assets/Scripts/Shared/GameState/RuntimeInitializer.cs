using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Shared.GameState
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
			foreach (var startup in startups)
			{
				startup.GameStart();
			}
		}
	}
}