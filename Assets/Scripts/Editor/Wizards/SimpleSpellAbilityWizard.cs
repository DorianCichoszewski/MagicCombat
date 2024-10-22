using System.Collections.Generic;
using MagicCombat.Gameplay.Abilities.Limiters;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Gameplay.Spell.Interface;
using MagicCombat.Gameplay.Spell.Property;
using MagicCombat.Implementations.Abilities;
using MagicCombat.Implementations.Spell.Event;
using MagicCombat.Implementations.Spell.LogicalFragment;
using Shared;
using UnityEditor;
using UnityEngine;

namespace MagicCombat.Editor.Wizards
{
	public class SimpleSpellAbilityWizard : ScriptableWizard
	{
		public string abilityName;

		[Header("Abilities Data")]
		public Sprite icon;

		public float spellOffset = 2f;
		public bool singleCast = true;

		[Header("SpellData")]
		public float speed;

		public float duration;
		public bool destroyOnHit;
		public GameObject spellGameObject;
		public SpellVisualFragment spellVisualFragment;

		[MenuItem("Magic Combat/Create/Simple Spell Ability")]
		public static void CreateSimpleWizard()
		{
			DisplayWizard<SimpleSpellAbilityWizard>("Simple Spell Ability Wizard", "Create");
		}

		private void OnWizardCreate()
		{
			string abilityPath = string.Format(Constants.AbilityFormat, abilityName);
			string spellPath = string.Format(Constants.SpellFromAbilityFormat, abilityName);

			var ability = GetSpellAbility();
			var spell = GetSpellPrototype();

			ability.spellPrototype = spell;
			AssetDatabase.CreateAsset(ability, abilityPath);
			AssetDatabase.CreateAsset(spell, spellPath);

			Selection.SetActiveObjectWithContext(spell, null);
		}

		protected virtual SpellAbility GetSpellAbility()
		{
			var ability = CreateInstance<SpellAbility>();
			ability.DefaultIcon = icon;
			ability.limiterProvider = LimiterProvider.Create(new CooldownLimiter { duration = 1f });
			;
			ability.offset = spellOffset;
			ability.singleCast = singleCast;
			return ability;
		}

		protected virtual SpellPrototype GetSpellPrototype()
		{
			var spell = CreateInstance<SpellPrototype>();
			spell.properties = new PropertyGroup();

			if (speed > 0)
			{
				spell.properties.Add(PropertyId.Speed, speed);
				spell.logicalFragments = new List<SpellLogicalFragment> { new StraightMoveSpellFragment() };
			}

			if (spellVisualFragment)
			{
				spell.visualFragments = new List<SpellVisualFragment> { spellVisualFragment };
			}

			if (spellGameObject)
			{
				spell.graphicalFragments = new List<GameObject> { spellGameObject };
			}

			if (duration > 0)
			{
				spell.properties.Add(PropertyId.Duration, duration);
				spell.useTimers = true;
				spell.timers = new List<SpellTimer>()
				{
					SpellTimer.Create(new List<ISpellEventGeneric> { new SelfDestroySpellEvent() })
				};
			}

			if (destroyOnHit)
			{
				spell.hitEventsType |= SpellHitEvent.All;
				spell.allHitEvents = new List<ISpellEventHit> { new SelfDestroySpellEvent() };
			}

			return spell;
		}
	}
}