using MagicCombat.Shared.Interfaces;
using MagicCombat.Shared.StageFlow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/One Time/Shared Scriptable", fileName = "Shared Scriptable")]
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
		private IModeData modeData;

		public StagesManager StagesManager => stagesManager;
		public IPlayerProvider PlayerProvider => playerProvider;

		public IModeData ModeData
		{
			get => modeData;
			set => modeData = value;
		}

		public EssentialsObject Essentials { get; private set; }


		public void EnsureEssentials()
		{
			if (Essentials != null) return;

			Essentials = Instantiate(essentialsPrefab);
			Essentials.Init(this);

			playerProvider = Essentials.GetComponentInChildren<IPlayerProvider>();
			stagesManager.Init(this);
		}
	}
}