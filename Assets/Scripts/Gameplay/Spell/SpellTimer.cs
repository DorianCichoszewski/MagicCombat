using System;
using System.Collections.Generic;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Spell
{
	[Serializable]
	public class SpellTimer : ISpellPropertiesUser
	{
		[SerializeField]
		[HorizontalGroup("Clock", Gap = 10)]
		private SpellBuilderValue duration;

		[SerializeField]
		[HideInInspector]
		private string name;

		[SerializeReference]
		private List<ISpellEventGeneric> events = new();

		public string Name
		{
			get => name;
			set => name = value;
		}

		public PropertyIdList RequiredProperties => new PropertyIdList().Add(duration);

		public Timer Start(SpellObject spell, Clock clock)
		{
			return clock.CreateTimer(() => PerformEvents(spell),
				duration.Evaluate(spell.Properties),
				$"{spell.name}'s timer");
		}

		private void PerformEvents(SpellObject spell)
		{
			foreach (var eventTrigger in events)
			{
				eventTrigger.Perform(spell);
			}
		}
	}
}