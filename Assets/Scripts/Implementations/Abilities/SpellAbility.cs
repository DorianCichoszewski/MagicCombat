using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Spell;
using Shared.Extension;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Implementations.Abilities
{
	[CreateAssetMenu(menuName = AbilitiesPath + Name, fileName = Name)]
	public class SpellAbility : BaseAbility
	{
		private const string Name = "Spell Cast";

		public float offset = 5f;
		public bool singleCast = true;

		[SerializeField]
		[Required]
		[InlineEditor]
		public SpellPrototype spellPrototype;

		protected override AbilityType Type => AbilityType.Basic;

		public override void Perform(AbilityCaster caster, AbilityState state)
		{
			var avatar = caster.Avatar;
			var spellData = new SpellData
			{
				AbilitiesContext = caster.AbilitiesContext,
				caster = caster,
				direction = avatar.MovementController.LookDirection,
				position = avatar.transform.position
			};

			spellData.position += (spellData.direction * offset).ToVec3();

			caster.AbilitiesContext.SpellCrafter.CreateNew(spellPrototype, spellData);

			state.onPerform?.Invoke();

			if (singleCast)
				state.onFinished?.Invoke();
			else
				state.isActive = true;
		}
	}
}