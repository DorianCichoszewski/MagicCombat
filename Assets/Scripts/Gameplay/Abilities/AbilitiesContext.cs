using System;
using MagicCombat.Shared.Time;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Gameplay.Abilities
{
	[Serializable]
	public class AbilitiesContext
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
		private ClockManager clockManager;

		public ClockManager ClockManager => clockManager;
		public SpellCrafter SpellCrafter => spellCrafter;
		public AbilitiesCollection AbilitiesCollection => abilitiesCollection;
		public StartAbilitiesData StartAbilitiesData => startAbilitiesData;
		
		public void Reset()
		{
			clockManager = new();
			abilitiesCollection = new AbilitiesCollection(abilitiesLabel);
		}
	}
}