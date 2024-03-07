using System;

namespace MagicCombat.Gameplay.Player.Ability
{
	public class AbilityState
	{
		public bool isActive;

		public Action onFailedPerform;

		public Action onFinished;

		public Action onNextClick;

		public Action onPerform;

		public Action<AbilityState> onStateChanged;
	}
}