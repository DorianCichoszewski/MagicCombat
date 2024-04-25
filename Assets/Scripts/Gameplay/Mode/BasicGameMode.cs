using DG.Tweening;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.StageFlow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Mode
{
	[CreateAssetMenu(menuName = "Magic Combat/One Time/Basic Game Mode", fileName = "Basic Game Mode")]
	public class BasicGameMode : GameMode
	{
		[SerializeField]
		private StageData endRoundStage;

		[SerializeField]
		private int pointsTarget;

		[Header("Debug view")]
		[ShowInInspector]
		[ReadOnly]
		private bool isPlaying;

		private SharedScriptable sharedScriptable;
		private GameplayManager manager;

		private GameplayRuntimeData GameplayData => (GameplayRuntimeData)sharedScriptable.ModeData;

		public override bool GameInProgress => isPlaying;

		public override void Init(SharedScriptable shared, GameplayManager manager)
		{
			sharedScriptable = shared;
			this.manager = manager;

			var playerProvider = sharedScriptable.PlayerProvider;

			foreach (var index in playerProvider.PlayersEnumerator)
			{
				var newPlayer = manager.CreatePlayer(playerProvider.StaticData(index),
					playerProvider.GameplayInputController(index), index);
				alivePlayers.Add(newPlayer);
			}

			isPlaying = true;
		}

		public override void PlayerHit(PlayerAvatar player)
		{
			if (!isPlaying) return;
			if (!alivePlayers.Contains(player)) return;

			alivePlayers.Remove(player);

			if (alivePlayers.Count > 1) return;

			// End game
			isPlaying = false;
			var winner = alivePlayers[0];
			GameplayData.points[winner.Id]++;
			EndGameAnimation(winner);
		}

		public override void SimulateGame()
		{
			while (true)
			{
				var randomPlayerIndex = sharedScriptable.PlayerProvider.GetRandomPlayer();
				GameplayData.points[randomPlayerIndex]++;
				if (GameplayData.points[randomPlayerIndex] >= pointsTarget)
					break;
			}
		}

		private void EndGameAnimation(PlayerAvatar winner)
		{
			var clockManager = GameplayData.AbilitiesContext.clockManager;
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

				OnGameEnd(winner);
			});

			s.Play();
		}

		private void OnGameEnd(PlayerAvatar winner)
		{
			GameplayData.points[winner.Id]++;
			sharedScriptable.StagesManager.GoToStage(endRoundStage);
		}
	}
}