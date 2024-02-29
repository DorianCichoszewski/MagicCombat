using UnityEngine;

namespace GameState
{
	public abstract class BaseState : MonoBehaviour
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