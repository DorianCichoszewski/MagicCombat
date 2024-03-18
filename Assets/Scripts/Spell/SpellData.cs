using MagicCombat.Gameplay;
using MagicCombat.Gameplay.Player;
using UnityEngine;

namespace MagicCombat.Spell
{
	public struct SpellData
	{
		public Vector2 direction;
		public Vector3 position;
		public PlayerAvatar caster;
		public GameplayGlobals gameplayGlobals;
	}
}