using Shared.Interfaces;
using Shared.StageFlow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Shared.GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/One Time/Shared Scriptable", fileName = "Shared Scriptable")]
	public class SharedScriptable : StartupScriptable
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
		private IModeData modeData;

		public StagesManager StagesManager => stagesManager;
		public IPlayerProvider PlayerProvider => playerProvider;

		public IModeData ModeData
		{
			get => modeData;
			set => modeData = value;
		}

		public EssentialsObject Essentials { get; private set; }

		public override void GameStart()
		{
			if (Essentials != null) Debug.LogError("SharedScriptable was already run!", Essentials);

			Essentials = Instantiate(essentialsPrefab);
			Essentials.Init(this);

			playerProvider = Essentials.GetComponentInChildren<IPlayerProvider>();
			stagesManager.Init(this);
		}
	}
}