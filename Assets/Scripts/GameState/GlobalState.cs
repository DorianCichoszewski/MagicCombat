using MagicCombat.Player;
using UnityEngine;

namespace MagicCombat.GameState
{
	public class GlobalState : MonoBehaviour
	{
		public ProjectScenes projectScenes;

		public PlayersManager playersManager;

		public void Init()
		{
			DontDestroyOnLoad(gameObject);
			playersManager.Init();

			projectScenes.onSceneChanged += ClearCallbacks;
		}

		private void ClearCallbacks()
		{
			playersManager.ClearEvents();
		}
	}
}