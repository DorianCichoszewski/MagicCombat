using Gameplay.Limiters;
using UnityEngine;

namespace Gameplay.Player.Ability
{
	public abstract class BaseAbility : ScriptableObject
	{
		public const string AbilitiesPath = "Abilities/";
		public const string UtilitiesPath = "Abilities/Utilities/";

		[SerializeField]
		private bool isMultiClick;

		[SerializeField]
		protected Sprite defaultIcon;
		
		public bool IsMultiClick => isMultiClick;
		public abstract Sprite GUIIcon { get; }

		public LimiterProvider limiterProvider;

		public abstract void Perform(PlayerBase caster, AbilityState state);
	}
}