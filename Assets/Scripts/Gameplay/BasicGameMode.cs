using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Notifications;
using MagicCombat.Gameplay.Player;
using Shared.Data;
using Shared.GameState;
using Shared.Services;
using Shared.StageFlow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Single/Data/Game Mode", fileName = "Basic Game Mode")]
	public class BasicGameMode : ScriptableService
	{
		[SerializeField]
		private EventChannelPlayerAvatar playerHitChannel;

		[Header("Debug view")]
		[ShowInInspector]
		[ReadOnly]
		private bool isPlaying;

		[ShowInInspector]
		[ReadOnly]
		protected List<PlayerAvatar> alivePlayers;

		private GameplayRuntimeData runtimeData;

		public List<PlayerAvatar> AlivePlayers => alivePlayers;
		
		public PlayerAvatar GetPlayer(UserId id) => alivePlayers.First(player => player.Id == id);

		public bool GameInProgress => isPlaying;

		public void Run(GameplayManager manager)
		{
			alivePlayers = new List<PlayerAvatar>();
			playerHitChannel.OnRaised += PlayerHit;

			runtimeData = ScriptableLocator.Get<GameplayRuntimeData>();
			var playerProvider = ScriptableLocator.Get<PlayerProvider>();

			foreach (var index in playerProvider.PlayersEnumerator)
			{
				var newPlayer = manager.CreatePlayer(index);
				alivePlayers.Add(newPlayer);
				runtimeData.PlayerCreatedChannel.Invoke(newPlayer);
			}

			isPlaying = true;
		}

		public void PlayerHit(PlayerAvatar player)
		{
			if (!isPlaying) return;
			if (!alivePlayers.Contains(player)) return;

			runtimeData.PlayerDeadChannel.Invoke(player);
			alivePlayers.Remove(player);

			if (alivePlayers.Count > 1) return;

			// End game
			isPlaying = false;
			var winner = alivePlayers[0];
			runtimeData.points[winner.Id]++;
			EndGameAnimation(winner);
		}

		private void EndGameAnimation(PlayerAvatar winner)
		{
			var abilitiesContext = ScriptableLocator.Get<AbilitiesContext>();
			var abilitiesClock = abilitiesContext.AbilitiesClock;
			abilitiesClock.Speed = 0f;

			var s = DOTween.Sequence();

			s.Append(DOTween.To(() => abilitiesClock.Speed,
				x => abilitiesClock.Speed = x,
				1f, 2f).SetEase(Ease.InCubic).Done());

			s.AppendInterval(1f);

			s.OnComplete(() =>
			{
				abilitiesClock.Speed = 1f;
				OnGameEnd(winner);
			});

			s.Play();
		}

		private void OnGameEnd(PlayerAvatar winner)
		{
			runtimeData.points[winner.Id]++;
			ScriptableLocator.Get<StagesManager>().NextStage();
		}
	}
}