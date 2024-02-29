using Gameplay.Abilities;
using Gameplay.Player.Ability;
using Gameplay.Player.Basic;
using Player;
using UnityEngine;

namespace Gameplay.Player
{
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField]
		private MovementController movement;

		[SerializeField]
		private SkinController skin;

		[Space]
		[SerializeField]
		private BaseAbility utilityDef;
		[SerializeField]
		private BaseAbility skill1Def;
		[SerializeField]
		private BaseAbility skill2Def;
		[SerializeField]
		private BaseAbility skill3Def;
		
		public AbilityCaster utility;
		public AbilityCaster skill1;
		public AbilityCaster skill2;
		public AbilityCaster skill3;

		private GameplayManager gameplayManager;
		private PlayerController playerController;

		public MovementController MovementController => movement;
		public PlayerController PlayerController => playerController;

		public GameplayGlobals GameplayGlobals => gameplayManager.GameplayGlobals;

		public void Init(PlayerController controller, GameplayManager manager)
		{
			gameplayManager = manager;
			playerController = controller;
			skin.SetSkin(controller.Data);
			movement.Init(manager.GameplayGlobals);
			
			movement.Enabled = true;
			
			utility = new AbilityCaster(this, utilityDef);
			skill1 = new AbilityCaster(this, skill1Def);
			skill2 = new AbilityCaster(this, skill2Def);
			skill3 = new AbilityCaster(this, skill3Def);
		}

		public void Hit()
		{
			gameplayManager.OnPlayerDeath(playerController);
		}
	}
}