using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicCombat.GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/Project Scenes", fileName = "Project Scenes")]
	public class ProjectScenes : ScriptableObject
	{
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
#if UNITY_EDITOR
		[SerializeField]
		private SceneAsset settingPlayers;

		[SerializeField]
		private SceneAsset settingAbilities;

		[SerializeField]
		private SceneAsset gameplay;

		private void OnValidate()
		{
			settingPlayersName = settingPlayers != null ? settingPlayers.name : "";
			settingAbilitiesName = settingAbilities != null ? settingAbilities.name : "";
			gameplayName = gameplay != null ? gameplay.name : "";
		}
#endif
	}
}