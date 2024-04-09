using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Player;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Magic Combat/GameplayRuntimeData", fileName = "GameplayRuntimeData")]
	public class GameplayRuntimeData : AbstractGameModeData
	{
		[SerializeField]
		[Required]
		private PlayerAvatar playerPrefab;
		
		[SerializeField]
		[Required]
		private StartAbilitiesData startAbilitiesData;
		
		[Space]
		public PerPlayerData<GameplayPlayerData> playerData = new();
		public PerPlayerData<int> points = new();
		
		[SerializeField]
		private AbilitiesContext abilitiesContext;

		public AbilitiesContext AbilitiesContext => abilitiesContext;
		public PlayerAvatar PlayerPrefab => playerPrefab;

		public AbilitiesGroup AbilitiesGroup => startAbilitiesData.AbilitiesGroup;

		public void Reset()
		{
			abilitiesContext.Reset();
			
			playerData.Reset();
			points.Reset();
		}
	}
}