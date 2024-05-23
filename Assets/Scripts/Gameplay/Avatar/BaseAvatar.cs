using System;
using MagicCombat.Gameplay.Avatar.Movement;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Extension;
using MagicCombat.Shared.Interfaces;
using MagicCombat.Shared.TimeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Avatar
{
	public class BaseAvatar : MonoBehaviour, ISpellTarget
	{
		[SerializeField]
		private MovementController movement;

		[SerializeField]
		private SkinController skin;

		[ShowInInspector]
		[ReadOnly]
		public bool Alive { get; private set; } = true;

		public MovementController MovementController => movement;

		public Vector2 Position => transform.position.ToVec2();

		public StaticPlayerData InitData { get; private set; }

		public event Action OnDeath;

		public void Init(StaticPlayerData initData, ClockFixedUpdate clockManager)
		{
			InitData = initData;
			skin.SetSkin(initData);
			movement.Init(clockManager);
			Alive = true;

			movement.enabled = true;
		}

		public void Kill()
		{
			if (!Alive) return;

			Alive = false;
			movement.enabled = false;
			OnDeath?.Invoke();
		}

		public void AddForce(Vector2 force, float forceDuration = 0)
		{
			var movementForce = new MovementForce
			{
				duration = forceDuration,
				type = MovementForceType.Constant
			};
			MovementController.AddForce(movementForce.GetNew(force));
		}
	}
}