using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	public abstract class BaseManager : MonoBehaviour
	{
		[SerializeField]
		[Required]
		protected SharedScriptable sharedScriptable;

		[SerializeField]
		[Required]
		public AbstractGameModeData expectedGameMode;

		public SharedScriptable SharedScriptable => sharedScriptable;
		public AbstractGameModeData ExpectedGameMode => expectedGameMode;

		private void Awake()
		{
			sharedScriptable.EnsureEssentials();
			OnAwake();
			sharedScriptable.RegisterNewManager(this);
		}

		protected virtual void OnAwake() { }
	}
}