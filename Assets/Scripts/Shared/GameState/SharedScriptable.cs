using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	[CreateAssetMenu(menuName = "Magic Combat/Shared Scriptable", fileName = "Shared Scriptable")]
	public class SharedScriptable : ScriptableObject
	{
		[SerializeField]
		[AssetsOnly]
		private GlobalState essentialsPrefab;
		
		[SerializeField]
		[AssetsOnly]
		private ProjectScenes projectScenes;

		private GlobalState createdEssentials;

		public ProjectScenes ProjectScenes => projectScenes;
		public GlobalState Essentials => createdEssentials;
		
		public void EnsureEssentials()
		{
			if (createdEssentials != null) return;
			createdEssentials = Instantiate(essentialsPrefab);
			createdEssentials.Init(this);
		}

		public void RegisterNewManager(BaseManager manager)
		{
			Essentials.RegisterNewManager(manager);
		}

		public void OnValidate()
		{
			projectScenes.Validate();
		}
	}
}