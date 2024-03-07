using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.Player
{
	public class PlayerInputMapper : MonoBehaviour
	{
		[SerializeField]
		private PlayerController controller;

		public void Move(InputAction.CallbackContext ctx)
		{
			if (!controller.EnableInput) return;

			controller.Avatar.MovementController.Move(ctx);
		}

		public void Rotate(InputAction.CallbackContext ctx)
		{
			if (!controller.EnableInput) return;

			controller.Avatar.MovementController.Rotate(ctx);
		}

		public void Utility(InputAction.CallbackContext ctx)
		{
			if (!controller.EnableInput) return;

			controller.Avatar.utility.PerformFromInput(ctx);
		}

		public void Skill1(InputAction.CallbackContext ctx)
		{
			if (!controller.EnableInput) return;

			controller.Avatar.skill1.PerformFromInput(ctx);
		}

		public void Skill2(InputAction.CallbackContext ctx)
		{
			if (!controller.EnableInput) return;

			controller.Avatar.skill2.PerformFromInput(ctx);
		}

		public void Skill3(InputAction.CallbackContext ctx)
		{
			if (!controller.EnableInput) return;

			controller.Avatar.skill3.PerformFromInput(ctx);
		}
	}
}