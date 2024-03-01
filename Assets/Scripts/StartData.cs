using System;
using System.Collections.Generic;
using Gameplay.Abilities;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic Combat/StartData")]
public class StartData : ScriptableObject
{
	public List<PlayerInit> playerInitList;

	[Serializable]
	public class PlayerInit
	{
		public string name;
		public Material material;
		public Color color;
		public Vector3 spawnPos;
		
		public BaseAbility startUtility;
		public BaseAbility startSkill1;
		public BaseAbility startSkill2;
		public BaseAbility startSkill3;
	}
}