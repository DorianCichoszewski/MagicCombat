using Gameplay.Abilities;
using MagicCombat.Extension;
using MagicCombat.Gameplay.Player;
using MagicCombat.Gameplay.Player.Ability;
using MagicCombat.Spell;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	[CreateAssetMenu(menuName = AbilitiesPath + Name + "Spell", fileName = Name)]
	public class FireballSpellAbility : BaseAbility
	{
		private const string Name = "Fireball";

		[SerializeField]
		private SpellPrototype fireballPrototype;

		[SerializeField]
		private float offset = 2f;

		public override void Perform(PlayerAvatar caster, AbilityState state)
		{
			var spellData = new SpellData
			{
				gameplayGlobals = caster.GameplayGlobals,
				caster = caster,
				direction = caster.MovementController.LookDirection,
				position = caster.transform.position
			};
			
			spellData.position += (spellData.direction * offset).ToVec3();
			
			caster.GameplayGlobals.spellCrafter.CreateNew(fireballPrototype, spellData);
			
			state.onPerform?.Invoke();
			state.onFinished?.Invoke();
		}
	}
}