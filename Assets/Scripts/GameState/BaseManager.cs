using UnityEngine;

namespace MagicCombat.GameState
{
	public abstract class BaseManager : MonoBehaviour
	{
		[SerializeField]
		protected RuntimeScriptable runtimeScriptable;

		public RuntimeScriptable RuntimeScriptable => runtimeScriptable;

		private void Awake()
		{
			runtimeScriptable.EnsureEssentials();
			OnAwake();
		}

		protected virtual void OnAwake() { }
	}
}