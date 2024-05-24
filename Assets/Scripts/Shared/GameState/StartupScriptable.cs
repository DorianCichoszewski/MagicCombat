using UnityEngine;

namespace Shared.GameState
{
	public abstract class StartupScriptable : ScriptableObject
	{
		public abstract void GameStart();
	}
}