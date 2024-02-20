using Gameplay.Limiters;
using UnityEngine;

namespace Gameplay.Player.Ability
{
	public abstract class BaseAbility : ScriptableObject
	{
		public const string AbilitiesPath = "Abilities/";
		public const string UtilitiesPath = "Abilities/Utilities/";

		public Sprite icon;

		public LimiterProvider limiterProvider;

		public abstract void Perform(PlayerBase caster);
	}
}