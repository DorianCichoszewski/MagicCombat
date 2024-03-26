using System.Collections.Generic;
using MagicCombat.Gameplay.Abilities.Base;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	[CreateAssetMenu(menuName = "Abilities/Abilities Group", fileName = "Abilities Group", order = -100)]
	public class AbilitiesGroup : ScriptableObject
	{
		[SerializeField]
		private List<BaseAbility> abilities = new();

		public List<BaseAbility> Abilities => abilities;
		
		public BaseAbility GetAbility(int index)
		{
			return abilities[index];
		}
	}
}