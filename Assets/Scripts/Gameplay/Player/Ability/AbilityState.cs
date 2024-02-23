using System;
using UnityEngine;

namespace Gameplay.Player.Ability
{
	public class AbilityState
	{
		public bool isActive;

		public Func<Transform> getSpellTransform;

		public Action onPerform;

		public Action onFailedPerform;

		public Action onFinished;
	}
}