using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Interface
{
	public interface ISpellData
	{
		public Vector2 Direction { get; }
		public Vector3 Position { get; }
		public ISpellTarget CasterSpellTarget { get; }
	}
}