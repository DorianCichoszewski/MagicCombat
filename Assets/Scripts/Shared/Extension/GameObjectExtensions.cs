using UnityEngine;

namespace MagicCombat.Shared.Extension
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