using System;
using UnityEngine;

namespace MagicCombat.Shared.Data
{
	[Serializable]
	public class StaticPlayerData
	{
		public string name;
		public Material material;
		public Color color;
		public Vector2 spawnPos;
	}
}