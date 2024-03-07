using Gameplay.Abilities;
using Gameplay.Player.Ability;
using Gameplay.Player.Basic;
using Gameplay.Player.Movement;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Player
{
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField]
		private MovementController movement;

		[SerializeField]
		private SkinController skin;
		
		[SerializeField, ReadOnly]
		private bool alive = true;
		
		public AbilityCaster utility;
		public AbilityCaster skill1;
		public AbilityCaster skill2;
		public AbilityCaster skill3;

		private GameplayManager gameplayManager;
		private PlayerController playerController;

		public MovementController MovementController => movement;
		public PlayerController PlayerController => playerController;
		public bool Alive => alive;

		public GameplayGlobals GameplayGlobals => gameplayManager.GameplayGlobals;

		public void Init(PlayerData playerData, GameplayManager manager)
		{
			gameplayManager = manager;
			playerController = playerData.controller;
			skin.SetSkin(playerController.InitData);
			movement.Init(manager.GameplayGlobals);
			
			movement.enabled = true;
			
			utility = new AbilityCaster(this, playerData.utility);
			skill1 = new AbilityCaster(this, playerData.skill1);
			skill2 = new AbilityCaster(this, playerData.skill2);
			skill3 = new AbilityCaster(this, playerData.skill3);
		}

		public void Kill()
		{
			if (!alive) return;

			alive = false;
			movement.enabled = false;
			playerController.EnableInput = false;
			gameplayManager.OnPlayerDeath(playerController);
		}
	}
}