using System;
using UnityEngine;

namespace Shared.Data
{
	[Serializable]
	public class StaticUserData
	{
		public string name;
		public Material material;
		public Color color;
		public Vector2 spawnPos;
	}
}