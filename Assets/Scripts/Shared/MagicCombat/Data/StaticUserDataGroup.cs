using System.Collections.Generic;
using Shared.Services;
using UnityEngine;

namespace Shared.Data
{
	[CreateAssetMenu(menuName = "Single/Global Data/Static Player Data Group", fileName = "Static Player Data")]
	public class StaticUserDataGroup : ScriptableService
	{
		public List<StaticUserData> staticUserDatas;
		
		public StaticUserData Get(UserId id) => staticUserDatas[id.OrderedId];
	}
}