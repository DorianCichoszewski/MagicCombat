using System;
using System.Collections.Generic;
using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Event
{
	[Serializable]
	internal class AbilityChangeIconEvent : ISpellEventGeneric, ISpellEventPlayerHit
	{
		[SerializeField]
		private Sprite icon;
		
		public List<PropertyId> RequiredProperties => null;
		
		public void Perform(SpellObject spell, ISpellTarget target)
		{
			((SpellData)spell.Data).caster.State.icon = icon;
		}

		public void Perform(SpellObject spell)
		{
			((SpellData)spell.Data).caster.State.icon = icon;
		}
	}
}