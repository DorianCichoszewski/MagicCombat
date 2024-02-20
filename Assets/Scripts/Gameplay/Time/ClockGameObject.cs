using UnityEngine;

namespace Gameplay.Time
{
	public class ClockGameObject : MonoBehaviour
	{
		[SerializeField]
		private GameplayGlobals globals;

		private void Awake()
		{
			if (ClockManager.instance != null)
			{
				Destroy(gameObject);
				return;
			}

			ClockManager.instance = globals.clockManager;

			DontDestroyOnLoad(gameObject);
		}

		private void Update()
		{
			globals.clockManager.DynamicClock.UpdateClock(UnityEngine.Time.deltaTime);
		}

		private void FixedUpdate()
		{
			globals.clockManager.FixedClock.UpdateClock(UnityEngine.Time.fixedDeltaTime);
		}
	}
}