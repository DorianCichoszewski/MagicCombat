using System;
using MagicCombat.Shared.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Gameplay.Abilities
{
	[Serializable]
	public class AbilitiesContext : IDisposable
	{
		[SerializeField]
		[Required]
		[AssetsOnly]
		private SpellCrafter spellCrafter;

		[SerializeField]
		[Required]
		private StartAbilitiesData startAbilitiesData;

		[SerializeField]
		[Required]
		private AssetLabelReference abilitiesLabel;

		[Required]
		private AbilitiesCollection abilitiesCollection;

		[ShowInInspector]
		private ClockUpdate abilitiesClock;

		[ShowInInspector]
		private ClockFixedUpdate physicClock;

		public ClockUpdate AbilitiesClock => abilitiesClock;
		public ClockFixedUpdate PhysicClock => physicClock;
		public SpellCrafter SpellCrafter => spellCrafter;
		public AbilitiesCollection AbilitiesCollection => abilitiesCollection;
		public StartAbilitiesData StartAbilitiesData => startAbilitiesData;

		public void Init()
		{
			abilitiesClock = new ClockUpdate();

			physicClock = new ClockFixedUpdate();
			abilitiesCollection = new AbilitiesCollection(abilitiesLabel);
		}

		public void Dispose()
		{
			abilitiesClock?.Dispose();
			physicClock?.Dispose();
		}
	}
}