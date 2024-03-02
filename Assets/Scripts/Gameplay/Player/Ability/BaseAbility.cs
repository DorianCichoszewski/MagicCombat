using Gameplay.Limiters;
using Gameplay.Player;
using Gameplay.Player.Ability;
using UnityEngine;

namespace Gameplay.Abilities
{
	public abstract class BaseAbility : ScriptableObject
	{
		public const string AbilitiesPath = "Abilities/";
		public const string UtilitiesPath = "Abilities/Utilities/";

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