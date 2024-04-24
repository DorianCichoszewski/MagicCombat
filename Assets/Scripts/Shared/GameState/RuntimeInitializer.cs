using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Shared.GameState
{
	public class RuntimeInitializer
	{
		const string StartupScriptableLabel = "{StartupScriptable}";
		
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void RunStartupScriptables()
		{
			AssetLabelReference label = new AssetLabelReference
			{
				labelString = StartupScriptableLabel
			};
			var operationHandle = Addressables.LoadAssetsAsync<StartupScriptable>(label, scriptable =>
			{
				scriptable.GameStart();
			});
		}
	}
}