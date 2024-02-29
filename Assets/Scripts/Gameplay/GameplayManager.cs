using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Player;
using Gameplay.Time;
using Gameplay.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
	public class GameplayManager : MonoBehaviour
	{
		[SerializeField]
		private StartData startData;
		[SerializeField]
		private GameplayGlobals gameplayGlobals;
		[SerializeField]
		private ClockGameObject clockGO;

		[SerializeField]
		private GameplayUI gameplayUI;
		[SerializeField]
		private GameStartUI gameStartUI;

		private readonly List<PlayerController> currentPlayers = new();

		public GameplayGlobals GameplayGlobals => gameplayGlobals;

		public event Action<PlayerController> AddedPlayer;
		public event Action GameStarted;

		private void OnEnable()
		{
			gameplayGlobals.Init();
			clockGO.Init(gameplayGlobals.clockManager);
		}

		public void OnAddPlayer(PlayerInput playerInput)
		{
			int playerIndex = currentPlayers.Count;
			var player = playerInput.GetComponent<PlayerController>();

			currentPlayers.Add(player);
			player.Init(startData.playerInitList[playerIndex], this, playerIndex);
			AddedPlayer?.Invoke(player);
		}

		public void StartGame()
		{
			for (int i = 0; i < currentPlayers.Count; i++)
			{
				var player = currentPlayers[i];
				player.StartGame();
			}
			GameStarted?.Invoke();
		}

		public void OnPlayerDeath(PlayerController player)
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