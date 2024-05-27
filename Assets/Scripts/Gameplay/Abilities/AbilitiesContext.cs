using Shared.Data;
using Shared.Services;
using Shared.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Gameplay.Abilities
{
	[CreateAssetMenu(menuName = "Single/Data/Abilities Context", fileName = "Abilities Context")]
	public class AbilitiesContext : ScriptableService
	{
		[SerializeField]
		[Required]
		[AssetsOnly]
		private SpellCrafter spellCrafter;

		[SerializeField]
		[Required]
		private AssetLabelReference abilitiesLabel;
		
		[SerializeField]
		private InitialAbilities initialAbilities;

		[ShowInInspector]
		[ReadOnly]
		private AbilitiesCollection abilitiesCollection;
		
		[ShowInInspector]
		[ReadOnly]
		private PerPlayerData<AbilityPlayerData> abilitiesData;

		[ShowInInspector]
		private ClockUpdate abilitiesClock;

		[ShowInInspector]
		private ClockFixedUpdate physicClock;

		public ClockUpdate AbilitiesClock => abilitiesClock;
		public ClockFixedUpdate PhysicClock => physicClock;
		public SpellCrafter SpellCrafter => spellCrafter;
		public AbilitiesCollection AbilitiesCollection => abilitiesCollection;
		public PerPlayerData<AbilityPlayerData> AbilitiesData => abilitiesData;
		public InitialAbilities InitialAbilities => initialAbilities;

		public override void OnRegister()
		{
			abilitiesCollection = new AbilitiesCollection(abilitiesLabel);
			abilitiesData = new PerPlayerData<AbilityPlayerData>(new AbilityPlayerData(initialAbilities));
			abilitiesClock ??= new ClockUpdate();
			abilitiesClock.Clear();

			physicClock ??= new ClockFixedUpdate();
			physicClock.Clear();
		}
		
		public void ResetClocks()
		{
			abilitiesClock.Clear();
			physicClock.Clear();
		}

		public override void OnDeregister()
		{
			abilitiesClock.Clear();
			physicClock.Clear();
		}
	}
}