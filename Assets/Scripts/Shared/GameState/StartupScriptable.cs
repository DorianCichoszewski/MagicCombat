using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	public abstract class StartupScriptable : ScriptableObject
	{
		public abstract void GameStart();
	}
}