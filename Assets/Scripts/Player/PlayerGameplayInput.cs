using System;
using MagicCombat.Shared.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MagicCombat.Player
{
	public class PlayerGameplayInput : MonoBehaviour, IGameplayInputController
	{
		[SerializeField]
		private PlayerInputController controller;

		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnRotate;
		public event Action OnUtility;
		public event Action OnSkill1;
		public event Action OnSkill2;
		public event Action OnSkill3;

		public void Clear()
		{
			OnMove = null;
			OnRotate = null;
			OnUtility = null;
			OnSkill1 = null;
			OnSkill2 = null;
			OnSkill3 = null;
		}

		public void Move(InputAction.CallbackContext ctx)
		{
			var value = ctx.ReadValue<Vector2>();

			OnMove?.Invoke(value);
		}

		public void Rotate(InputAction.CallbackContext ctx)
		{
			var value = ctx.ReadValue<Vector2>();

			OnRotate?.Invoke(value);
		}

		public void Utility(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			OnUtility?.Invoke();
		}

		public void Skill1(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			OnSkill1?.Invoke();
		}

		public void Skill2(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			OnSkill2?.Invoke();
		}

		public void Skill3(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;

			OnSkill3?.Invoke();
		}
	}
}