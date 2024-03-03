using GameState;
using UnityEngine;

namespace SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		private int minPlayers = 2;
		[SerializeField]
		private int maxPlayers = 4;

		public void ConfirmPlayers()
		{
			int currentPlayers = runtimeScriptable.playersData.Count;
			if (currentPlayers < minPlayers || currentPlayers > maxPlayers)
				return;
			
			Debug.Log("Finished Setting Players");
			runtimeScriptable.Essentials.projectScenes.GoToSettingAbilities();
		}
	}
}