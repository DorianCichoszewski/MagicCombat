using MagicCombat.GameState;
using UnityEngine;

namespace MagicCombat.SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		private int minPlayers = 2;

		[SerializeField]
		private int maxPlayers = 4;

		protected override void OnAwake()
		{
			runtimeScriptable.playersData.Clear();
		}

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