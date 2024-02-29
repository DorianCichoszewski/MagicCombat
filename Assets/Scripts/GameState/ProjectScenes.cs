using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/Project Scenes", fileName = "Project Scenes")]
	public class ProjectScenes : ScriptableObject
	{
#if UNITY_EDITOR
		[SerializeField]
		private UnityEditor.SceneAsset settingPlayers;
		[SerializeField]
		private UnityEditor.SceneAsset settingAbilities;
		[SerializeField]
		private UnityEditor.SceneAsset gameplay;

		private void OnValidate()
		{
			settingPlayersName = settingPlayers != null ? settingPlayers.name : "";
			settingAbilitiesName = settingAbilities != null ? settingAbilities.name : "";
			gameplayName = gameplay != null ? gameplay.name : "";
		}
#endif

		[Header("Scenes names - set automatically")]
		[SerializeField]
		private string settingPlayersName;
		[SerializeField]
		private string settingAbilitiesName;
		[SerializeField]
		private string gameplayName;

		public event Action onSceneChanged;

		public void GoToSettingPlayers()
		{
			onSceneChanged?.Invoke();
			SceneManager.LoadScene(settingPlayersName, LoadSceneMode.Single);
		}
		
		public void GoToSettingAbilities()
		{
			onSceneChanged?.Invoke();
			SceneManager.LoadScene(settingAbilitiesName, LoadSceneMode.Single);
		}
		
		public void GoToGameplay()
		{
			onSceneChanged?.Invoke();
			SceneManager.LoadScene(gameplayName, LoadSceneMode.Single);
		}
	}
}