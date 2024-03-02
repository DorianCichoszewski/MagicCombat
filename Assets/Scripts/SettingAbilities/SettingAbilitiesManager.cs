using GameState;
using UnityEngine;

namespace SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		[SerializeField]
		private SettingAbilitiesUI ui;
		
		protected override void OnAwake()
		{
			ui.Init();
		}

		public void Next()
		{
			Debug.Log("Finished abilities setup");
			runtimeScriptable.Essentials.projectScenes.GoToGameplay();
		}
	}
}