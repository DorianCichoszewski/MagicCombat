using System;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities.Base
{
	public class AbilityState
	{
		public bool isActive;
		public Sprite icon;

		public Action onFailedPerform;

		public Action onFinished;

		public Action onNextClick;

		public Action onPerform;

		public Action<AbilityState> onStateChanged;
	}
}