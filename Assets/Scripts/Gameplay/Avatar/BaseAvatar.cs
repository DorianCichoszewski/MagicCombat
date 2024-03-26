using MagicCombat.Gameplay.Avatar.Movement;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
using MagicCombat.Shared.Time;
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

		public ClockManager ClockManager { get; private set; }
		public StaticPlayerData InitData { get; private set; }

		public void Init(StaticPlayerData initData, ClockManager clockManager)
		{
			InitData = initData;
			ClockManager = clockManager;
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
		}
	}
}