using Gameplay.Player;
using Gameplay.UI.Ability;
using Player;
using UnityEngine;

namespace Gameplay.UI
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

		private PlayerController player;

		private void Awake()
		{
			gameObject.SetActive(false);
		}

		public void SetPlayer(PlayerController player)
		{
			this.player = player;
		}

		public void Init()
		{
			if (player == null) return;
			
			utilityIndicator.SetupAbilityCaster(player.Avatar.utility);
			skill1Indicator.SetupAbilityCaster(player.Avatar.skill1);
			skill2Indicator.SetupAbilityCaster(player.Avatar.skill2);
			skill3Indicator.SetupAbilityCaster(player.Avatar.skill3);

			gameObject.SetActive(true);
		}
	}
}