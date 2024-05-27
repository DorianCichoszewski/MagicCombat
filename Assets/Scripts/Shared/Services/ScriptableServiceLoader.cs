using System;
using System.Collections.Generic;
using Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Shared.Services
{
	// Allows scriptable to be accessible in the static context
	// Stores only one per type
	[CreateAssetMenu(menuName = "Single/Startup/Service Locator")]
	internal class ScriptableServiceLoader : StartupScriptable
	{
		[SerializeField]
		[Required]
		private AssetLabelReference servicesLabel;
		
		[ShowInInspector]
		[ReadOnly]
		private readonly List<ScriptableService> loadedServicesDebugList = new();

		private readonly Dictionary<Type, ScriptableService> loadedServices = new();
		
		public override void GameStart()
		{
			Clear();
			var services = Addressables.LoadAssetsAsync<ScriptableService>(servicesLabel, _ => { }).WaitForCompletion();
			foreach (var service in services)
			{
				RegisterService(service);
			}

			ScriptableLocator.ScriptableServiceLoader = this;
		}

		public T GetService<T>() where T : ScriptableService
		{
			if (loadedServices.TryGetValue(typeof(T), out var service))
			{
				return (T)service;
			}

			throw new Exception($"Service {typeof(T)} not found");
		}

		public bool TryGetService<T>(out T service) where T : ScriptableService
		{
			bool ret = loadedServices.TryGetValue(typeof(T), out var serviceBase);
			service = (T)serviceBase;
			return ret;
		}
		
		public void RegisterService(ScriptableService service)
		{
			if (service == null)
				throw new Exception("Trying to register null service");
			loadedServicesDebugList.Add(service);
			loadedServices.Add(service.GetType(), service);
			service.OnRegister();
		}
		
		public void RegisterService<T>(ScriptableService service)
		{
			loadedServicesDebugList.Add(service);
			loadedServices.Add(typeof(T), service);
			service.OnRegister();
		}
		
		public void DeregisterService(ScriptableService service)
		{
			loadedServicesDebugList.Remove(service);
			loadedServices.Remove(service.GetType());
			service.OnDeregister();
		}

		private void Clear()
		{
			loadedServices.Clear();
			loadedServicesDebugList.Clear();
		}
	}
}