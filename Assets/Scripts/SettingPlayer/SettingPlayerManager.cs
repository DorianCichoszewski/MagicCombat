using System;
using MagicCombat.Gameplay;
using MagicCombat.Shared.GameState;
using UnityEngine;

namespace MagicCombat.SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		private int minPlayers = 2;

		[SerializeField]
		private int maxPlayers = 4;

		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.GameModeData;

		public event Action OnRefreshPlayers;

		protected override void OnAwake()
		{
			GameModeData?.Reset();
		}

		public void RefreshPlayers()
		{
			OnRefreshPlayers?.Invoke();
		}

		public void ConfirmPlayers()
		{
			int currentPlayers = sharedScriptable.PlayerProvider.PlayersCount;
			if (currentPlayers < minPlayers || currentPlayers > maxPlayers)
				return;

			Debug.Log("Finished Setting Players");
			sharedScriptable.ProjectScenes.GoToSettingAbilities();
		}
	}
}