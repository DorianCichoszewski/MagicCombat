using UnityEngine;

namespace Shared.Services
{
	public abstract class ScriptableService : ScriptableObject
	{
		public virtual void OnRegister() { }
		
		public virtual void OnDeregister() { }
		
		public static T Get<T>() where T : ScriptableService
		{
			return ScriptableLocator.Get<T>();
		}
	}
}