using MagicCombat.Gameplay.Player;
using MagicCombat.Gameplay.UI.Ability;
using UnityEngine;

namespace MagicCombat.Gameplay.UI
{
	public class PlayerUI : MonoBehaviour
	{
		[SerializeField]
		private AbilityIndicator utilityIndicator;

		[SerializeField]
		private AbilityIndicator skill1Indicator;

		[SerializeField]
		private AbilityIndicator skill2Indicator;

		[SerializeField]
		private AbilityIndicator skill3Indicator;

		private PlayerAvatar player;

		private void Awake()
		{
			gameObject.SetActive(false);
		}

		public void Init(PlayerAvatar playerAvatar)
		{
			player = playerAvatar;

			utilityIndicator.SetupAbilityCaster(player.utility);
			skill1Indicator.SetupAbilityCaster(player.skill1);
			skill2Indicator.SetupAbilityCaster(player.skill2);
			skill3Indicator.SetupAbilityCaster(player.skill3);

			gameObject.SetActive(true);

			player.OnDeath += PlayerDeath;
		}

		private void PlayerDeath()
		{
			gameObject.SetActive(false);
		}
	}
}