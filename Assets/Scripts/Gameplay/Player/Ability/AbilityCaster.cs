using System;
using Gameplay.Limiters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Player.Ability
{
	public class AbilityCaster : MonoBehaviour
	{
		[SerializeField]
		private PlayerBase caster;

		public BaseAbility ability;

		public event Action<bool> TriedPerform;

		private ILimiter limiter;

		public ILimiter Limiter
		{
			get
			{
				SetupLimiter();
				return limiter;
			}
		}

		public void Awake()
		{
			SetupLimiter();
		}

		private void SetupLimiter()
		{
			if (limiter != null) return;
			limiter = ability.limiterProvider.Limiter;
			limiter.Reset();
		}

		public void PerformFromInput(InputAction.CallbackContext ctx)
		{
			if (!ctx.performed) return;
			
			TryToCast();
		}

		public void TryToCast()
		{
			if (limiter.TryPerform())
			{
				ability.Perform(caster);
				TriedPerform?.Invoke(true);
			}
			else
			{
				TriedPerform?.Invoke(false);
			}
		}
	}
}