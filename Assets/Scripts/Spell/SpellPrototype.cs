using System;
using System.Collections.Generic;
using System.Linq;
using MagicCombat.Extension;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Spell
{
	[CreateAssetMenu(menuName = "Magic Combat/Spell", fileName = "New Spell")]
	public class SpellPrototype : SerializedScriptableObject
	{
		public PropertyGroup properties = new();
		
		[Space(20)]
		[Title("Fragments")]
		[AssetsOnly]
		public List<GameObject> graphicalFragments = new();

		public List<SpellVisualFragment> visualFragments = new();
		
		public List<SpellLogicalFragment> logicalFragments = new();

		[Space(20)]
		[Title("Events")]
		public bool useTimers;

		[ShowIf(nameof(useTimers))]
		public List<SpellTimer> timers = new();

		[Space]
		public bool useDestroyEvents;
		
		[ShowIf(nameof(useDestroyEvents))]
		public List<ISpellEventGeneric> destroyEvents = new();

		[Space]
		[EnumToggleButtons]
		public SpellHitEvent hitEventsType = SpellHitEvent.None;
		
		[ShowIf(nameof(UsePlayerHitEvents))]
		public List<ISpellEventPlayerHit> playerHitEvents = new();

		[ShowIf(nameof(UseOtherHitEvents))]
		public List<ISpellEventHit> otherHitEvents = new();

		[ShowIf(nameof(UseAllHitEvents))]
		public List<ISpellEventHit> allHitEvents = new();

		private List<PropertyId> CombinedProperties
		{
			get
			{
				return visualFragments
					.Concat<ISpellPropertiesUser>(logicalFragments)
					.Concat(timers)
					.Concat(destroyEvents)
					.Concat(playerHitEvents)
					.Concat(otherHitEvents)
					.Concat(allHitEvents)
					.Where(user => user is { RequiredProperties: not null })
					.SelectMany(user => user.RequiredProperties)
					.Distinct()
					.ToList();
			}
		}

		public bool UsePlayerHitEvents => hitEventsType.HasFlag(SpellHitEvent.Player);
		public bool UseOtherHitEvents => hitEventsType.HasFlag(SpellHitEvent.Other);
		public bool UseAllHitEvents => hitEventsType.HasFlag(SpellHitEvent.All);

		private void OnValidate()
		{
			RefreshProperties();
		}

		private void RefreshProperties()
		{
			var newPropertiesId = CombinedProperties;
			var newProperties = new PropertyGroup();
			foreach (var propertyId in newPropertiesId)
			{
				if (properties.TryGetValue(propertyId, out float property))
					newProperties.Add(propertyId, property);
				else
					newProperties.Add(propertyId, 0);
			}

			properties = newProperties;
		}

		public Spell CreateNew(SpellData data)
		{
			var spellGO = new GameObject("");
			spellGO.transform.SetPositionAndRotation(data.position, data.direction.ToRotation());
			var spell = spellGO.AddComponent<Spell>();
			spell.Init(this, data);
			return spell;
		}
	}

	[Flags]
	public enum SpellHitEvent
	{
		None = 0,
		Player = 1 << 0,
		Other = 1 << 1,
		All = 1 << 2
	}
}