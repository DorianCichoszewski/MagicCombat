using MagicCombat.GameState;
using UnityEngine;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		public void Next()
		{
			Debug.Log("Finished abilities setup");
			runtimeScriptable.Essentials.projectScenes.GoToGameplay();
		}
	}
}