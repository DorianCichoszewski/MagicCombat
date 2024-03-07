using MagicCombat.Gameplay.Limiters;
using MagicCombat.Gameplay.Player;
using MagicCombat.Gameplay.Player.Ability;
using UnityEngine;

namespace Gameplay.Abilities
{
	public abstract class BaseAbility : ScriptableObject
	{
		protected const string AbilitiesPath = "Abilities/";
		protected const string UtilitiesPath = "Abilities/Utilities/";

		[SerializeField]
		private bool isMultiClick;

		[SerializeField]
		protected Sprite defaultIcon;

		public LimiterProvider limiterProvider;

		public bool IsMultiClick => isMultiClick;

		public virtual Sprite SelectionIcon => defaultIcon;

		public virtual Sprite GetIcon(AbilityState state)
		{
			return defaultIcon;
		}

		public abstract void Perform(PlayerAvatar caster, AbilityState state);
	}
}