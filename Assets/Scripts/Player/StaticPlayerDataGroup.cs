using System.Collections.Generic;
using Shared.Data;
using Shared.Services;
using UnityEngine;

namespace MagicCombat.Player
{
	[CreateAssetMenu(menuName = "Single/Global Data/Static Player Data Group", fileName = "Static Player Data")]
	public class StaticPlayerDataGroup : ScriptableService
	{
		public List<StaticPlayerData> staticPlayerDatas;
		
		public StaticPlayerData Get(PlayerId id) => staticPlayerDatas[id.OrderedId];
	}
}