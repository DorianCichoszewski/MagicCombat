using Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell.Interface
{
	public interface ISpellData
	{
		public Vector2 Direction { get; set; }
		public Vector3 Position { get; set; }
		public ISpellTarget CasterSpellTarget { get; }
	}
}