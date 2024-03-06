using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Time;
using GameState;
using Player;
using UnityEngine;

namespace Gameplay
{
	public class GameplayManager : BaseManager
	{
		[SerializeField]
		private GameplayGlobals gameplayGlobals;
		[SerializeField]
		private ClockGameObject clockGO;
		
		public GameplayGlobals GameplayGlobals => gameplayGlobals;
		
		private List<PlayerController> deadPlayers;
		
		public event Action GameStarted;

		protected override void OnAwake()
		{
			gameplayGlobals.Init();
			clockGO.Init(gameplayGlobals.clockManager);
			
			StartGame();
		}

		private void StartGame()
		{
			deadPlayers = new();
			foreach (var playerData in runtimeScriptable.playersData)
			{
				if (playerData.controller == null) continue;
				playerData.controller.CreateAvatar(this, playerData);
			}

			runtimeScriptable.Essentials.playersManager.onPlayerJoined += p => p.CreateAvatar(this, runtimeScriptable.GetPlayerData(p));

			GameStarted?.Invoke();
		}

		public void OnPlayerDeath(PlayerController player)
		{
			deadPlayers.Add(player);
			player.EnableInput = false;
			
			if (deadPlayers.Count < runtimeScriptable.playersData.Count - 1) return;
			
			foreach (var playerData in runtimeScriptable.playersData)
			{
				playerData.controller.EnableInput = false;
			}
			
			EndGameAnimation();
		}

		private void EndGameAnimation()
		{
			gameplayGlobals.clockManager.DynamicClock.CurrentSpeed = 0f;
			gameplayGlobals.clockManager.FixedClock.CurrentSpeed = 0f;

			DOTween.To(() => gameplayGlobals.clockManager.DynamicClock.CurrentSpeed,
				x => gameplayGlobals.clockManager.DynamicClock.CurrentSpeed = x,
				1f, 3f).SetEase(Ease.InCubic).Done().Play();
			DOTween.To(() => gameplayGlobals.clockManager.FixedClock.CurrentSpeed,
				x => gameplayGlobals.clockManager.FixedClock.CurrentSpeed = x,
				1f, 3f).SetEase(Ease.InCubic).Done().Play();
		}
	}
}