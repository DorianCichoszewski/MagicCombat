using UnityEngine;

namespace MagicCombat.Spell
{
	public interface ISpellData
	{
		public Vector2 Direction { get; }
		public Vector3 Position { get; }
	}
}