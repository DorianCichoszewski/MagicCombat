using System;
using MagicCombat.Extension;
using UnityEngine;

namespace MagicCombat.Spell
{
	[Serializable]
	public class SpellCrafter
	{
		public Spell CreateNew(SpellPrototype prototype, ISpellData data)
		{
			var spellGO = new GameObject("");
			spellGO.transform.SetPositionAndRotation(data.Position, data.Direction.ToRotation());
			var spell = spellGO.AddComponent<Spell>();
			spell.Init(prototype, data, ((SpellData)data).gameplayGlobals.clockManager);
			return spell;
		}
	}
}