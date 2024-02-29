using Player;
using UnityEngine;

namespace GameState
{
	public class GlobalState : MonoBehaviour
	{
		[SerializeField]
		private ProjectScenes projectScenes;
		
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