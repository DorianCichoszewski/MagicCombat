using System;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Shared.Extension;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MagicCombat.Gameplay.Abilities
{
	[Serializable]
	public class SpellCrafter
	{
		[SerializeField]
		private SpellObject spellPrototype;


		public SpellObject CreateNew(SpellPrototype prototype, ISpellData data)
		{
			var spell = Object.Instantiate(spellPrototype, data.Position, data.Direction.ToRotation());
			spell.transform.SetPositionAndRotation(data.Position, data.Direction.ToRotation());
			spell.Init(prototype, data, ((SpellData)data).AbilitiesContext.ClockManager);
			return spell;
		}
	}
}