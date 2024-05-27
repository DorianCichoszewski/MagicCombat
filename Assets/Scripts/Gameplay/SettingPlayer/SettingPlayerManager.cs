using System;
using Shared.GameState;
using Shared.Interfaces;
using Shared.Services;
using Shared.StageFlow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.SettingPlayer
{
	public class SettingPlayerManager : BaseManager
	{
		[SerializeField]
		[MinValue(1)]
		private int minPlayers = 2;

		private PlayerProvider playerProvider;

		private void Awake()
		{
			playerProvider = ScriptableLocator.Get<PlayerProvider>();
		}

		public void ConfirmPlayers()
		{
			while (playerProvider.PlayersCount < minPlayers)
			{
				playerProvider.AddBot();
			}

			ScriptableLocator.Get<StagesManager>().NextStage();
		}
	}
}