using System;
using System.Collections.Generic;
using DG.Tweening;
using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Extension;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.Interfaces;
using MagicCombat.Shared.Time;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	public class GameplayManager : BaseManager
	{
		[SerializeField]
		private GameplayContext gameplayContext;

		[SerializeField]
		private ClockGameObject clockGO;

		[ShowInInspector]
		[ReadOnly]
		private bool isPlaying;
		[ShowInInspector]
		[ReadOnly]
		private List<PlayerAvatar> alivePlayers = new();

		public event Action OnGameStarted;
		public event Action<PlayerAvatar> OnPlayerDeath;
		public event Action<PlayerAvatar> OnGameEnd;
		
		public AbilitiesContext AbilitiesContext => gameplayContext.AbilitiesContext;

		public GameplayContext GameplayContext => gameplayContext;

		public List<PlayerAvatar> AlivePlayers => alivePlayers;

		protected override void OnAwake()
		{
			gameplayContext.Init();
		}

		public void StartGame()
		{
			clockGO.Init(AbilitiesContext.clockManager);
			isPlaying = true;
			OnGameStarted?.Invoke();
		}

		public void PlayerDeath(PlayerAvatar player)
		{
			if (!isPlaying) return;
			if (!alivePlayers.Contains(player)) return;
			
			alivePlayers.Remove(player);
			OnPlayerDeath?.Invoke(player);

			if (alivePlayers.Count > 2) return;

			// End game
			player.Controller.EnabledInput = false;
			isPlaying = false;
			EndGameAnimation(alivePlayers[0]);
		}

		private void EndGameAnimation(PlayerAvatar winner)
		{
			var clockManager = AbilitiesContext.clockManager;
			clockManager.DynamicClock.CurrentSpeed = 0f;
			clockManager.FixedClock.CurrentSpeed = 0f;

			var s = DOTween.Sequence();

			s.Append(DOTween.To(() => clockManager.DynamicClock.CurrentSpeed,
				x => clockManager.DynamicClock.CurrentSpeed = x,
				1f, 2f).SetEase(Ease.InCubic).Done());
			s.Join(DOTween.To(() => clockManager.FixedClock.CurrentSpeed,
					x => clockManager.FixedClock.CurrentSpeed = x,
					1f, 2f)
				.SetEase(Ease.InCubic).Done());

			s.AppendInterval(1f);

			s.OnComplete(() =>
			{
				clockManager.DynamicClock.CurrentSpeed = 1f;
				clockManager.FixedClock.CurrentSpeed = 1f;

				OnGameEnd?.Invoke(winner);
			});

			s.Play();
		}

		public void CreatePlayer(GameplayPlayerData gameplayPlayerData, StaticPlayerData staticData, IGameplayInputController input, int id)
		{
			var playerPrefab = gameplayContext.PlayerPrefab;
			var player = Instantiate(playerPrefab, staticData.spawnPos.ToVec3(), Quaternion.identity);
			player.Init(gameplayPlayerData, staticData,this, input, id);
			alivePlayers.Add(player);
		}
	}
}