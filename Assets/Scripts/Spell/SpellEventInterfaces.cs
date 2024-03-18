using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Spell
{
	public interface ISpellEventGeneric : ISpellPropertiesUser
	{
		public void Perform(Spell spell);
	}

	public interface ISpellEventPlayerHit : ISpellPropertiesUser
	{
		public void Perform(Spell spell, PlayerAvatar player);
	}

	public interface ISpellEventHit : ISpellPropertiesUser
	{
		public void Perform(Spell spell, GameObject other);
	}
}