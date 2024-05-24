using System;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using Shared.Extension;
using Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Implementations.Spell.Event
{
	[Serializable]
	public class PushOtherEvent : ISpellEventPlayerHit
	{
		public enum Type
		{
			FromDirection,
			FromSpell,
			FromCaster
		}

		private const PropertyId Force = PropertyId.Force;

		[SerializeField]
		private Type directionType;

		[SerializeField]
		private float duration;

		public PropertyIdList RequiredProperties => new PropertyIdList().Add(Force);

		public void Perform(SpellObject spell, ISpellTarget target)
		{
			var direction = directionType switch
			{
				Type.FromDirection => spell.Data.Direction,
				Type.FromSpell => target.Position - spell.transform.position.ToVec2(),
				Type.FromCaster => target.Position - spell.Data.CasterSpellTarget.Position,
				_ => throw new ArgumentOutOfRangeException()
			};
			direction.Normalize();

			target.AddForce(spell.GetProperty(Force) * direction, duration);
		}
	}
}