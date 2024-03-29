using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicCombat.Shared.GameState
{
	[Serializable]
	public class ProjectScenes
	{
#if UNITY_EDITOR
		[SerializeField]
		private SceneAsset settingPlayers;

		[SerializeField]
		private SceneAsset settingAbilities;

		[SerializeField]
		private SceneAsset gameplay;
#endif
		
		[SerializeField]
		[ReadOnly]
		private string settingPlayersName;

		[SerializeField]
		[ReadOnly]
		private string settingAbilitiesName;

		[SerializeField]
		[ReadOnly]
		private string gameplayName;

		public event Action OnPreSceneChanged;
		public event Action OnPostSceneChanged;

		public void GoToSettingPlayers()
		{
			OnPreSceneChanged?.Invoke();
			SceneManager.LoadScene(settingPlayersName, LoadSceneMode.Single);
			OnPostSceneChanged?.Invoke();
		}

		public void GoToSettingAbilities()
		{
			OnPreSceneChanged?.Invoke();
			SceneManager.LoadScene(settingAbilitiesName, LoadSceneMode.Single);
			OnPostSceneChanged?.Invoke();
		}

		public void GoToGameplay()
		{
			OnPreSceneChanged?.Invoke();
			SceneManager.LoadScene(gameplayName, LoadSceneMode.Single);
			OnPostSceneChanged?.Invoke();
		}
		
		public void Validate()
		{
#if UNITY_EDITOR
			settingPlayersName = settingPlayers != null ? settingPlayers.name : "";
			settingAbilitiesName = settingAbilities != null ? settingAbilities.name : "";
			gameplayName = gameplay != null ? gameplay.name : "";
#endif
		}
	}
}