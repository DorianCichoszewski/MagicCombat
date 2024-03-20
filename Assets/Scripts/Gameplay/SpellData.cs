using MagicCombat.Gameplay;
using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Spell
{
	public class SpellData : ISpellData
	{
		public Vector2 direction;
		public Vector3 position;
		public PlayerAvatar caster;
		public GameplayGlobals gameplayGlobals;
		public Vector2 Direction => direction;
		public Vector3 Position => position;

		public ISpellData Copy()
		{
			return new SpellData
			{
				direction = direction,
				position = position,
				caster = caster,
				gameplayGlobals = gameplayGlobals
			};
		}
	}
}