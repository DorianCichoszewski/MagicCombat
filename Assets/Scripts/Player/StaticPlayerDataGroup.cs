using System.Collections.Generic;
using MagicCombat.Shared.Data;
using UnityEngine;

namespace MagicCombat.Player
{
	[CreateAssetMenu(menuName = "Magic Combat/One Time/Static Player Data Group", fileName = "Static Player Data")]
	public class StaticPlayerDataGroup : ScriptableObject
	{
		public List<StaticPlayerData> staticPlayerDatas;
	}
}