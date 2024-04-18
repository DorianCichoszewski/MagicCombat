using MagicCombat.Shared.Interfaces;
using MagicCombat.Shared.StageFlow;
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
		private StagesManager stagesManager;

		[ShowInInspector]
		[ReadOnly]
		private IPlayerProvider playerProvider;

		[ShowInInspector]
		[ReadOnly]
		private AbstractGameModeData gameModeData;

		public StagesManager StagesManager => stagesManager;
		public IPlayerProvider PlayerProvider => playerProvider;
		public AbstractGameModeData GameModeData => gameModeData;
		public EssentialsObject Essentials { get; private set; }


		public void EnsureEssentials()
		{
			if (Essentials != null) return;

			Essentials = Instantiate(essentialsPrefab);
			Essentials.Init(this);

			playerProvider = Essentials.GetComponentInChildren<IPlayerProvider>();
			stagesManager.Init(this);
		}

		public void RegisterNewManager(BaseManager manager)
		{
			if (gameModeData == null || gameModeData.GetType() != manager.ExpectedGameMode.GetType())
				gameModeData = manager.ExpectedGameMode;
		}
	}
}