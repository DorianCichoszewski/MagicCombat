using System;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/Shared Scriptable", fileName = "Shared Scriptable")]
	public class SharedScriptable : ScriptableObject
	{
		[SerializeField]
		[AssetsOnly]
		private EssentialsObject essentialsPrefab;

		[SerializeField]
		[AssetsOnly]
		private ProjectScenes projectScenes;

		[ShowInInspector]
		[ReadOnly]
		private IPlayerProvider playerProvider;

		[ShowInInspector]
		[ReadOnly]
		private AbstractGameModeData gameModeData;

		public event Action<BaseManager> OnNewRegisteredManager;

		public ProjectScenes ProjectScenes => projectScenes;
		public EssentialsObject Essentials { get; private set; }

		public IPlayerProvider PlayerProvider => playerProvider;
		public AbstractGameModeData GameModeData => gameModeData;


		public void EnsureEssentials()
		{
			if (Essentials != null) return;
			Essentials = Instantiate(essentialsPrefab);
			Essentials.Init(this);
			playerProvider = Essentials.GetComponentInChildren<IPlayerProvider>();
		}

		public void RegisterNewManager(BaseManager manager)
		{
			if (OnNewRegisteredManager == null) Debug.LogError("No script to run with manager. Game won't progress");

			if (gameModeData == null || gameModeData.GetType() != manager.ExpectedGameMode.GetType())
				gameModeData = manager.ExpectedGameMode;

			OnNewRegisteredManager?.Invoke(manager);
		}

		public void OnValidate()
		{
			projectScenes.Validate();
		}
	}
}