using System;
using MagicCombat.Gameplay;
using MagicCombat.Shared.Data;
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

		public GameplayRuntimeData GameModeData => (GameplayRuntimeData)sharedScriptable.ModeData;

		public event Action<PlayerId> OnRefreshPlayers;

		protected void Awake()
		{
			GameModeData?.Reset();
			sharedScriptable.PlayerProvider.OnPlayerChanged += RefreshPlayers;
		}

		public void RefreshPlayers(PlayerId player)
		{
			OnRefreshPlayers?.Invoke(player);
		}

		public void ConfirmPlayers()
		{
			int currentPlayers = sharedScriptable.PlayerProvider.PlayersCount;
			if (currentPlayers < minPlayers || currentPlayers > maxPlayers)
				return;

			sharedScriptable.StagesManager.NextStage();
		}
	}
}