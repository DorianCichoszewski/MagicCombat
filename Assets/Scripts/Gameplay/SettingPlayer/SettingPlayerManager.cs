using Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		[MinValue(1)]
		private int minPlayers = 2;

		public void ConfirmPlayers()
		{
			while (sharedScriptable.PlayerProvider.PlayersCount < minPlayers)
			{
				sharedScriptable.PlayerProvider.AddBot();
			}

			sharedScriptable.StagesManager.NextStage();
		}
	}
}