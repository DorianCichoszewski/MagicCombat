using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Shared.GameState
{
	public abstract class BaseManager : MonoBehaviour
	{
		[SerializeField]
		[Required]
		protected SharedScriptable sharedScriptable;

		public SharedScriptable SharedScriptable => sharedScriptable;

		private void Awake()
		{
			sharedScriptable.EnsureEssentials();
			OnAwake();
		}

		protected virtual void OnAwake() { }
	}
}