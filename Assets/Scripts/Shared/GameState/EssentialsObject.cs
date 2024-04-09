using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	public class EssentialsObject : MonoBehaviour
	{
		[ShowInInspector]
		[ReadOnly]
		private SharedScriptable sharedScriptablePreview;

		public void Init(SharedScriptable sharedScriptable)
		{
			DontDestroyOnLoad(gameObject);
			sharedScriptablePreview = sharedScriptable;
			var essentials = GetComponentsInChildren<IEssentialScript>();
			foreach (var essential in essentials)
			{
				essential.Init(sharedScriptable);
			}
		}
	}
}