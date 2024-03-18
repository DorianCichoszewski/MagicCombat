using System.Collections.Generic;
using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Spell.Event
{
	public class DestroyOtherSpellEvent : ISpellEventPlayerHit, ISpellEventHit
	{
		public void Perform(Spell spell, GameObject other)
		{
			Object.Destroy(other);
		}

		public void Perform(Spell spell, PlayerAvatar player)
		{
			player.Kill();
		}

		public List<PropertyId> RequiredProperties => new();
	}
}