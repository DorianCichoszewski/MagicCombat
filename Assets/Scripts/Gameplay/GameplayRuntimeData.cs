using MagicCombat.Gameplay.Notifications;
using MagicCombat.Gameplay.Player;
using Shared.Data;
using Shared.GameState;
using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Single/Data/Gameplay Runtime", fileName = "Gameplay Runtime Data")]
	public class GameplayRuntimeData : ScriptableService
	{
		[SerializeField]
		[Required]
		private PlayerAvatar playerPrefab;

		[SerializeField]
		private EventChannelPlayerAvatar playerCreatedChannel;

		[SerializeField]
		private EventChannelPlayerAvatar playerDeadChannel;

		[Space]
		[SerializeField]
		private int pointsTarget;

		[Space]
		public PerPlayerData<int> points;

		public PlayerAvatar PlayerPrefab => playerPrefab;
		public EventChannelPlayerAvatar PlayerCreatedChannel => playerCreatedChannel;
		public EventChannelPlayerAvatar PlayerDeadChannel => playerDeadChannel;

		public override void OnRegister()
		{
			points.Reset();
		}

		public void SimulateGame()
		{
			var playerProvider = ScriptableLocator.Get<PlayerProvider>();
			while (true)
			{
				var randomPlayerIndex = playerProvider.GetRandomUser();
				points[randomPlayerIndex]++;
				if (points[randomPlayerIndex] >= pointsTarget)
					break;
			}
		}
	}
}