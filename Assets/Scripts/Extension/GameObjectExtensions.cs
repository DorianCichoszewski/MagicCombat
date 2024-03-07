using UnityEngine;

namespace MagicCombat.Extension
{
	public static class GameObjectExtensions
	{
		public static void SetActiveCached(this GameObject gameObject, bool active)
		{
			if (gameObject.activeSelf == active) return;

			gameObject.SetActive(active);
		}
	}
}