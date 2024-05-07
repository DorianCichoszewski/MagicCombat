using System;
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

		public event Action<PlayerId> OnRefreshPlayers;

		protected void Awake()
		{
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