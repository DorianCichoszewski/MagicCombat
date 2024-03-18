using System.Collections.Generic;
using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Spell.Event
{
	public class SelfDestroySpellEvent : ISpellEventGeneric, ISpellEventHit, ISpellEventPlayerHit
	{
		public List<PropertyId> RequiredProperties => null;

		public void Perform(Spell spell)
		{
			spell.Destroy();
		}

		public void Perform(Spell spell, GameObject other)
		{
			spell.Destroy();
		}

		public void Perform(Spell spell, PlayerAvatar player)
		{
			spell.Destroy();
		}
	}
}