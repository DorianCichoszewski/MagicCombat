using System;
using System.Collections.Generic;
using DG.Tweening;
using MagicCombat.Gameplay.Time;
using MagicCombat.GameState;
using MagicCombat.Player;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayManager : BaseManager
	{
		[SerializeField]
		private GameplayGlobals gameplayGlobals;

		[SerializeField]
		private ClockGameObject clockGO;

		private List<PlayerController> deadPlayers;

		private bool isPlaying;

		public GameplayGlobals GameplayGlobals => gameplayGlobals;

		public event Action GameStarted;

		public event Action AvatarsChanged;

		protected override void OnAwake()
		{
			gameplayGlobals.Init();
			clockGO.Init(gameplayGlobals.clockManager);

			StartGame();
		}

		private void StartGame()
		{
			deadPlayers = new List<PlayerController>();
			foreach (var playerData in runtimeScriptable.playersData)
			{
				if (playerData.controller == null) continue;
				playerData.controller.CreateAvatar(this, playerData);
			}

			runtimeScriptable.Essentials.playersManager.onPlayerJoined += p =>
			{
				p.CreateAvatar(this, runtimeScriptable.GetPlayerData(p));
				AvatarsChanged?.Invoke();
			};

			isPlaying = true;

			GameStarted?.Invoke();
		}

		public void OnPlayerDeath(PlayerController player)
		{
			if (!isPlaying) return;
			isPlaying = false;
			deadPlayers.Add(player);
			player.EnableInput = false;

			if (deadPlayers.Count < runtimeScriptable.playersData.Count - 1) return;

			foreach (var playerData in runtimeScriptable.playersData)
			{
				//playerData.controller.EnableInput = false;
				if (playerData.controller.Avatar.Alive) playerData.points++;
			}

			EndGameAnimation();
		}

		private void EndGameAnimation()
		{
			gameplayGlobals.clockManager.DynamicClock.CurrentSpeed = 0f;
			gameplayGlobals.clockManager.FixedClock.CurrentSpeed = 0f;

			var s = DOTween.Sequence();

			s.Append(DOTween.To(() => gameplayGlobals.clockManager.DynamicClock.CurrentSpeed,
				x => gameplayGlobals.clockManager.DynamicClock.CurrentSpeed = x,
				1f, 2f).SetEase(Ease.InCubic).Done());
			s.Join(DOTween.To(() => gameplayGlobals.clockManager.FixedClock.CurrentSpeed,
					x => gameplayGlobals.clockManager.FixedClock.CurrentSpeed = x,
					1f, 2f)
				.SetEase(Ease.InCubic).Done());

			s.AppendInterval(1f);

			s.OnComplete(() =>
			{
				gameplayGlobals.clockManager.DynamicClock.CurrentSpeed = 1f;
				gameplayGlobals.clockManager.FixedClock.CurrentSpeed = 1f;

				runtimeScriptable.ProjectScenes.GoToSettingAbilities();
			});

			s.Play();
		}
	}
}