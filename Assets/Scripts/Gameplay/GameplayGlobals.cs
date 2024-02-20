using Gameplay.Time;
using UnityEngine;

namespace Gameplay
{
	[CreateAssetMenu(menuName = "Magic Combat/Gameplay Globals")]
	public class GameplayGlobals : ScriptableObject
	{
		public ClockManager clockManager;
	}
}