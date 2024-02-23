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

		private readonly List<PlayerBase> currentPlayers = new();

		public GameplayGlobals GameplayGlobals => gameplayGlobals;

		private void OnEnable()
		{
			gameplayGlobals.Init();
			clockGO.Init(gameplayGlobals.clockManager);
		}

		public void OnAddPlayer(PlayerInput playerInput)
		{
			int playerIndex = currentPlayers.Count;
			var player = playerInput.GetComponent<PlayerBase>();

			currentPlayers.Add(player);
			player.Init(startData.playerInitList[playerIndex], this);
			gameplayUI.PlayerSetup(player, playerIndex);
		}

		public void OnPlayerDeath(PlayerBase player)
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