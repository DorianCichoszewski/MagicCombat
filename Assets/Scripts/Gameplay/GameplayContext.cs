using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay
{
	[CreateAssetMenu(menuName = "Magic Combat/Gameplay Context")]
	public class GameplayContext : ScriptableObject
	{
		[SerializeField]
		private AbilitiesContext abilitiesContext;

		[SerializeField]
		[Required]
		private PlayerAvatar playerPrefab;
		
		[SerializeField]
		[Required]
		private StartAbilitiesData startAbilitiesData;

		public AbilitiesContext AbilitiesContext => abilitiesContext;
		public PlayerAvatar PlayerPrefab => playerPrefab;

		public AbilitiesGroup AbilitiesGroup => startAbilitiesData.AbilitiesGroup;

		public void Init()
		{
			abilitiesContext.Init();
		}
	}
}