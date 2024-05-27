using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Player;
using Shared.Data;
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

		[Space]
		public PerPlayerData<AbilityPlayerData> abilitiesData;

		public PerPlayerData<int> points;

		[SerializeField]
		private AbilitiesContext abilitiesContext;

		public AbilitiesContext AbilitiesContext => abilitiesContext;
		public PlayerAvatar PlayerPrefab => playerPrefab;

		public override void OnRegister()
		{
			abilitiesContext.Init();

			abilitiesData =
				new PerPlayerData<AbilityPlayerData>(new AbilityPlayerData(abilitiesContext.StartAbilitiesData));
			points.Reset();
		}

		public override void OnDeregister()
		{
			abilitiesContext.Dispose();
		}
	}
}