using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	public class EssentialsObject : MonoBehaviour
	{
		public void Init(SharedScriptable sharedScriptable)
		{
			DontDestroyOnLoad(gameObject);
			var essentials = GetComponentsInChildren<IEssentialScript>();
			foreach (var essential in essentials)
			{
				essential.Init(sharedScriptable);
			}
		}
	}
}