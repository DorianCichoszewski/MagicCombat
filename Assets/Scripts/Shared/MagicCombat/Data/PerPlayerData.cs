using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Shared.Data
{
	[Serializable]
	public class PerPlayerData<T> : IEnumerable<KeyValuePair<int, T>>
	{
		private T defaultData;

		[ShowInInspector]
		[ReadOnly]
		private Dictionary<int, T> createdData;

		public T this[UserId id]
		{
			get => createdData[id];
			set => createdData[id] = value;
		}

		public PerPlayerData(T defaultData)
		{
			this.defaultData = defaultData;
			createdData = new Dictionary<int, T>();
		}

		public T GetOrCreate(UserId id, T defaultData)
		{
			createdData.TryAdd(id, defaultData);

			return createdData[id];
		}

		public T GetOrCreate(UserId id)
		{
			return GetOrCreate(id, defaultData);
		}

		public void Create(UserId id, T data)
		{
			if (!createdData.TryAdd(id, data)) createdData[id] = data;
		}

		public void Create(UserId id)
		{
			Create(id, defaultData);
		}

		public void Set(UserId id, T data)
		{
			createdData[id] = data;
		}

		public void Reset()
		{
			createdData = new Dictionary<int, T>();
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