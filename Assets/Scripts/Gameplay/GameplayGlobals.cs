using MagicCombat.Gameplay.Time;
using MagicCombat.Spell;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Magic Combat/Gameplay Globals")]
	public class GameplayGlobals : ScriptableObject
	{
		public ClockManager clockManager;

		public SpellCrafter spellCrafter;

		public void Init()
		{
			clockManager.Init();
		}
	}
}