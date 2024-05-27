namespace Shared.Services
{
	public static class ScriptableLocator
	{
		internal static ScriptableServiceLoader ScriptableServiceLoader { private get; set; }

		public static T Get<T>() where T : ScriptableService
		{
			return ScriptableServiceLoader.GetService<T>();
		}

		public static bool TryGetService<T>(out T service) where T : ScriptableService
		{
			return ScriptableServiceLoader.TryGetService(out service);
		}
		
		public static void RegisterService<T>(ScriptableService service)
		{
			ScriptableServiceLoader.RegisterService<T>(service);
		}
		
		public static void RegisterService(ScriptableService service)
		{
			ScriptableServiceLoader.RegisterService(service);
		}
		
		public static void DeregisterService(ScriptableService service)
		{
			ScriptableServiceLoader.DeregisterService(service);
		}
	}
}