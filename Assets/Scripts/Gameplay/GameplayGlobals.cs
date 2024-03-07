using MagicCombat.Gameplay.Time;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Magic Combat/Gameplay Globals")]
	public class GameplayGlobals : ScriptableObject
	{
		public ClockManager clockManager;

		public void Init()
		{
			clockManager.Init();
		}
	}
}