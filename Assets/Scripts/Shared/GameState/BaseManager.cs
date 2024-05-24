using Sirenix.OdinInspector;
using UnityEngine;

namespace Shared.GameState
{
	public abstract class BaseManager : MonoBehaviour
	{
		[SerializeField]
		[Required]
		protected SharedScriptable sharedScriptable;

		public SharedScriptable SharedScriptable => sharedScriptable;
	}
}