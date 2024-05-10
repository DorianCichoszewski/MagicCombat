using System;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		[MinValue(1)]
		private int minPlayers = 2;

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
			while (sharedScriptable.PlayerProvider.PlayersCount < minPlayers)
			{
				sharedScriptable.PlayerProvider.AddBot();
			}

			sharedScriptable.StagesManager.NextStage();
		}
	}
}