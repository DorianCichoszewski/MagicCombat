using System.Collections.Generic;
using Gameplay.Abilities;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	[CreateAssetMenu(menuName = "Abilities/Abilities Group", fileName = "Abilities Group", order = -100)]
	public class AbilitiesGroup : ScriptableObject
	{
		[SerializeField]
		private List<BaseAbility> abilities = new();

		public List<BaseAbility> Abilities => abilities;
	}
}