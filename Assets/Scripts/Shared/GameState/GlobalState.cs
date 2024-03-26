using System;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	public class GlobalState : MonoBehaviour
	{
		[ShowInInspector]
		[ReadOnly]
		private SharedScriptable sharedScriptable;

		public SharedScriptable SharedScriptable => sharedScriptable;
		
		public event Action<BaseManager> OnNewRegisteredManager;

		public void Init(SharedScriptable sharedScriptable)
		{
			DontDestroyOnLoad(gameObject);
			this.sharedScriptable = sharedScriptable;
			var essentials = GetComponentsInChildren<IEssentialScript>();
			foreach (var essential in essentials)
			{
				essential.Init(this);
			}
		}

		public void RegisterNewManager(BaseManager manager)
		{
			if (OnNewRegisteredManager == null)
			{
				Debug.LogError("No script to run with manager. Game won't progress");
			}
			
			OnNewRegisteredManager?.Invoke(manager);
		}
	}
}