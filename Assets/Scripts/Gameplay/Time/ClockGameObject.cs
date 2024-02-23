using UnityEngine;

namespace Gameplay.Time
{
	public class ClockGameObject : MonoBehaviour
	{
		private ClockManager clockManager;

		public void Init(ClockManager manager)
		{
			clockManager = manager;
		}

		private void Update()
		{
			clockManager?.DynamicClock.UpdateClock(UnityEngine.Time.deltaTime);
		}

		private void FixedUpdate()
		{
			clockManager?.FixedClock.UpdateClock(UnityEngine.Time.fixedDeltaTime);
		}
	}
}