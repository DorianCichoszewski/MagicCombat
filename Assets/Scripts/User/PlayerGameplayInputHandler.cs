using Shared.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.User
{
	internal class PlayerGameplayInputHandler : MonoBehaviour
	{
		private GameplayInputMapping gameplayInputMapping;

		public void InitMapping(GameplayInputMapping mapping)
		{
			gameplayInputMapping = mapping;
		}

		public void Move(InputAction.CallbackContext ctx)
		{
			var value = ctx.ReadValue<Vector2>();

			gameplayInputMapping.Move(value);
		}

		public void Rotate(InputAction.CallbackContext ctx)
		{
			var value = ctx.ReadValue<Vector2>();

			gameplayInputMapping.Rotate(value);
		}

		public void Utility(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			gameplayInputMapping.CastUtility();
		}

		public void Skill1(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			gameplayInputMapping.CastSkill1();
		}

		public void Skill2(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			gameplayInputMapping.CastSkill2();
		}

		public void Skill3(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			gameplayInputMapping.CastSkill3();
		}
	}
}