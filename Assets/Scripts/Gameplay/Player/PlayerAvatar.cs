using System;
using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Avatar.Movement;
using Shared.Data;
using Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Player
{
	public class PlayerAvatar : MonoBehaviour
	{
		[SerializeField]
		private BaseAvatar avatar;

		[ReadOnly]
		[ShowInInspector]
		public PlayerId Id { get; private set; }

		private GameplayManager gameplayManager;

		[ShowInInspector]
		public AbilityCaster skill1;

		[ShowInInspector]
		public AbilityCaster skill2;

		[ShowInInspector]
		public AbilityCaster skill3;

		[ShowInInspector]
		public AbilityCaster utility;

		public PlayerController Controller { get; private set; }

		public MovementController MovementController => avatar.MovementController;
		public Vector2 Position => avatar.Position;

		public bool Alive => avatar.Alive;

		public event Action OnDeath;

		public void Init(AbilityPlayerData abilityPlayerData, StaticPlayerData staticData, GameplayManager manager,
			IGameplayInputController input, PlayerId id)
		{
			gameplayManager = manager;
			Id = id;
			avatar.Init(staticData, gameplayManager.AbilitiesContext.PhysicClock);
			avatar.OnDeath += OnAvatarDeath;

			var abilitiesData = gameplayManager.AbilitiesContext;

			var abilitiesContext = gameplayManager.GameModeData.AbilitiesContext;

			utility = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.UtilityKey],
				abilitiesData);
			skill1 = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.Skill1Key],
				abilitiesData);
			skill2 = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.Skill2Key],
				abilitiesData);
			skill3 = new AbilityCaster(avatar, abilitiesContext.AbilitiesCollection[abilityPlayerData.Skill3Key],
				abilitiesData);

			Controller = new PlayerController(this, input)
			{
				EnabledInput = true
			};
		}

		private void OnAvatarDeath()
		{
			OnDeath?.Invoke();
			Controller = null;
			gameplayManager.PlayerHit(this);
		}
	}
}