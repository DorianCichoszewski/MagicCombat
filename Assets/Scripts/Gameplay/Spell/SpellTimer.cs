using System;
using System.Collections.Generic;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Shared.Time;
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
		[HorizontalGroup("Clock")]
		[HideLabel]
		private ClockType clockType;

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

		public Timer Start(SpellObject spell, ClockManager clockManager)
		{
			return new Timer(name, duration.Evaluate(spell.Properties), () => PerformEvents(spell),
				clockManager);
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