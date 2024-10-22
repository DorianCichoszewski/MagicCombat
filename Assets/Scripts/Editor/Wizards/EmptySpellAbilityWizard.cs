using MagicCombat.Gameplay.Abilities.Limiters;
using MagicCombat.Gameplay.Spell;
using MagicCombat.Implementations.Abilities;
using Shared;
using UnityEditor;
using UnityEngine;

namespace MagicCombat.Editor.Wizards
{
	public class EmptySpellAbilityWizard : ScriptableWizard
	{
		public string abilityName;
		[Header("Abilities Data")]
		public Sprite icon;
		public float spellOffset = 2f;
		public bool singleCast = true;

		[MenuItem("Magic Combat/Create/Empty Spell Ability")]
		public static void CreateWizard()
		{
			DisplayWizard<EmptySpellAbilityWizard>("Empty Spell Ability Wizard", "Create");
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
			ability.limiterProvider = LimiterProvider.Create(new CooldownLimiter{duration = 1f});;
			ability.offset = spellOffset;
			ability.singleCast = singleCast;
			return ability;
		}

		protected virtual SpellPrototype GetSpellPrototype()
		{
			return CreateInstance<SpellPrototype>();
		}
	}
}