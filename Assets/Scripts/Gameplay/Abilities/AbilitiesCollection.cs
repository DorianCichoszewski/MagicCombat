using System;
using System.Collections.Generic;
using MagicCombat.Gameplay.Abilities.Base;
using Sirenix.OdinInspector;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Gameplay.Abilities
{
	[Serializable]
	public class AbilitiesCollection
	{
		private Dictionary<string, BaseAbility> abilitiesCache;

		[ShowInInspector]
		[ReadOnly]
		private List<BaseAbility> loadedAbilities;
		private List<string> loadedAbilityKeys;
		
		public List<BaseAbility> Abilities => loadedAbilities;
		
		public BaseAbility this[string key] => abilitiesCache[key];
		public BaseAbility this[int index] => loadedAbilities[index];
		
		public string GetKey(int index) => loadedAbilityKeys[index];

		public AbilitiesCollection(AssetLabelReference abilitiesLabel)
		{
			var syncLocations = Addressables.LoadResourceLocationsAsync(abilitiesLabel).WaitForCompletion();
			
			var tempLocationIdDatabase = new Dictionary<string, BaseAbility>();
			foreach (var syncLocation in syncLocations)
			{
				var asyncOperationHandle = Addressables.LoadAssetAsync<BaseAbility>(syncLocation.PrimaryKey);
				asyncOperationHandle.Completed +=
					handle => tempLocationIdDatabase.Add(syncLocation.InternalId, handle.Result);
				asyncOperationHandle.WaitForCompletion();
			}
			
			abilitiesCache = new();
			foreach (var resourceLocator in Addressables.ResourceLocators)
			{
				foreach (var key in resourceLocator.Keys)
				{
					bool hasLocation = resourceLocator.Locate(key, typeof(BaseAbility), out var keyLocations);
					if (!hasLocation || keyLocations.Count == 0)
						continue;
					if (keyLocations.Count > 1)
						continue;
 
					string keyLocationId = keyLocations[0].InternalId;
					if (!tempLocationIdDatabase.TryGetValue(keyLocationId, out var asset))
						continue;
 
					abilitiesCache.Add(key.ToString(), (BaseAbility)asset);
				}
			}

			loadedAbilities = new();
			loadedAbilityKeys = new();
			foreach (var cachedAbility in abilitiesCache)
			{
				if (loadedAbilities.Contains(cachedAbility.Value))
					continue;
				loadedAbilities.Add(cachedAbility.Value);
				loadedAbilityKeys.Add(cachedAbility.Key);
			}
		}
		
		public int GetIndex(string key)
		{
			return abilitiesCache.TryGetValue(key, out var ability) ? loadedAbilities.IndexOf(ability) : -1;
		}
	}

	[Serializable]
	internal class AssetReferenceAbility : AssetReferenceT<BaseAbility>
	{
		public AssetReferenceAbility(string guid) : base(guid) { }
	}
}