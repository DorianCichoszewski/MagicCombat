using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace MagicCombat.Shared.Data
{
	[Serializable]
	public class PerPlayerData<T> : IEnumerable<KeyValuePair<int, T>>
	{
		public T defaultData;

		[ShowInInspector]
		private Dictionary<int, T> createdData = new ();

		public T this[int id]
		{
			get => createdData[id];
			set => createdData[id] = value;
		}

		public T GetOrCreate(int id, T defaultData)
		{
			createdData.TryAdd(id, defaultData);

			return createdData[id];
		}
		
		public T GetOrCreate(int id)
		{
			return GetOrCreate(id, defaultData);
		}

		public void Create(int id, T data)
		{
			if (!createdData.TryAdd(id, data))
			{
				createdData[id] = data;
			}
		}
		
		public void Create(int id)
		{
			Create(id, defaultData);
		}

		public void Reset()
		{
			createdData = new();
		}

		public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
		{
			return createdData.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}