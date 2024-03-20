using MagicCombat.Gameplay.Player.Ability;
using MagicCombat.Gameplay.Player.Basic;
using MagicCombat.Gameplay.Player.Movement;
using MagicCombat.Player;
using MagicCombat.Spell;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Player
{
	public class PlayerAvatar : MonoBehaviour, ISpellTarget
	{
		[SerializeField]
		private MovementController movement;

		[SerializeField]
		private SkinController skin;

		[SerializeField]
		[ReadOnly]
		private bool alive = true;

		private GameplayManager gameplayManager;

		public AbilityCaster skill1;
		public AbilityCaster skill2;
		public AbilityCaster skill3;
		public AbilityCaster utility;

		public MovementController MovementController => movement;
		public PlayerController PlayerController { get; private set; }

		public bool Alive => alive;

		public GameplayGlobals GameplayGlobals => gameplayManager.GameplayGlobals;

		public void Init(PlayerData playerData, GameplayManager manager, GameplayGlobals globals)
		{
			gameplayManager = manager;
			PlayerController = playerData.controller;
			skin.SetSkin(PlayerController.InitData);
			movement.Init(manager.GameplayGlobals);

			movement.enabled = true;

			utility = new AbilityCaster(this, playerData.utility, globals);
			skill1 = new AbilityCaster(this, playerData.skill1, globals);
			skill2 = new AbilityCaster(this, playerData.skill2, globals);
			skill3 = new AbilityCaster(this, playerData.skill3, globals);
		}

		public void Kill()
		{
			if (!alive) return;

			alive = false;
			movement.enabled = false;
			PlayerController.EnableInput = false;
			gameplayManager.OnPlayerDeath(PlayerController);
		}
	}
}