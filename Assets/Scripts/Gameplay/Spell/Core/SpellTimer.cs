using System;
using System.Collections.Generic;
using MagicCombat.Gameplay.Time;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Spell
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

		public List<PropertyId> RequiredProperties
		{
			get
			{
				var properties = new List<PropertyId>();
				if (duration.useProperty)
					properties.Add(duration.property);
				return properties;
			}
		}

		public Timer Start(Spell spell, ClockManager clockManager)
		{
			return new Timer(name, duration.Evaluate(spell.Properties), () => PerformEvents(spell),
				clockManager);
		}

		private void PerformEvents(Spell spell)
		{
			foreach (var eventTrigger in events)
			{
				eventTrigger.Perform(spell);
			}
		}
	}
}