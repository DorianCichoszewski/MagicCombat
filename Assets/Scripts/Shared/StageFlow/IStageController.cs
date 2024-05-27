using System;
using System.Collections.Generic;
using Shared.GameState;
using Shared.Services;
using UnityEngine;

namespace Shared.StageFlow
{
	[Serializable]
	public abstract class StageController
	{
		private List<ScriptableService> loadedScriptables = new();

		// At entering as target stage
		public virtual void Run()
		{
			Enter();
		}

		// At skipping stage
		public virtual void Skip() { }

		// Entering only to go to child stage
		public virtual void Enter() { }

		// At closing child stage and returning focus
		public virtual void Return()
		{
			Debug.LogError("This stage doesn't support returning focus");
		}

		// At closing stage
		public virtual void Exit()
		{
			foreach (var scriptable in loadedScriptables)
			{
				ScriptableLocator.DeregisterService(scriptable);
			}

			loadedScriptables.Clear();
		}


		protected void LoadScriptable(ScriptableService scriptable)
		{
			ScriptableLocator.RegisterService(scriptable);
			loadedScriptables.Add(scriptable);
		}
	}
}