using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Mode;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Magic Combat/One Time/Gameplay Runtime Data", fileName = "Gameplay Runtime Data")]
	public class GameplayRuntimeData : ScriptableObject, IModeData
	{
		[SerializeField]
		[Required]
		private GameMode gameMode;

		[SerializeField]
		[Required]
		private PlayerAvatar playerPrefab;

		[SerializeField]
		[Required]
		private StartAbilitiesData startAbilitiesData;

		[Space]
		public PerPlayerData<GameplayPlayerData> playerData = new();

		public PerPlayerData<int> points;

		[SerializeField]
		private AbilitiesContext abilitiesContext;

		public AbilitiesContext AbilitiesContext => abilitiesContext;
		public PlayerAvatar PlayerPrefab => playerPrefab;

		public GameMode GameMode => gameMode;

		public AbilitiesGroup AbilitiesGroup => startAbilitiesData.AbilitiesGroup;

		public void Reset()
		{
			abilitiesContext.Reset();

			playerData.Reset();
			points.Reset();
		}
	}
}