using System;

namespace Gameplay.Player.Ability
{
	public class AbilityState
	{
		public bool isActive;

		public Action<AbilityState> onStateChanged;

		public Action onFailedPerform;

		public Action onFinished;

		public Action onPerform;

		public Action onNextClick;
	}
}