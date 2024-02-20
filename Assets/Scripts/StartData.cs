using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic Combat/StartData")]
public class StartData : ScriptableObject
{
	public List<PlayerInit> playerInitList;

	[Serializable]
	public class PlayerInit
	{
		public Material material;
		public Vector3 spawnPos;
	}
}