using System;
using MagicCombat.Gameplay.Abilities.Base;
using MagicCombat.Gameplay.Avatar;
using MagicCombat.Gameplay.Avatar.Movement;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.Interfaces;
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

		public AbilityCaster skill1;
		public AbilityCaster skill2;
		public AbilityCaster skill3;
		public AbilityCaster utility;

		public PlayerController Controller { get; private set; }

		public MovementController MovementController => avatar.MovementController;
		public Vector2 Position => avatar.Position;

		public bool Alive => avatar.Alive;

		public event Action OnDeath;

		public void Init(GameplayPlayerData gameplayPlayerData, StaticPlayerData staticData, GameplayManager manager,
			IGameplayInputController input, PlayerId id)
		{
			gameplayManager = manager;
			Id = id;
			avatar.Init(staticData, gameplayManager.AbilitiesContext.clockManager);
			avatar.OnDeath += OnAvatarDeath;

			var abilitiesData = gameplayManager.AbilitiesContext;

			var abilitiesGroup = gameplayManager.GameModeData.AbilitiesGroup;

			utility = new AbilityCaster(avatar, abilitiesGroup.GetAbility(gameplayPlayerData.UtilityIndex),
				abilitiesData);
			skill1 = new AbilityCaster(avatar, abilitiesGroup.GetAbility(gameplayPlayerData.Skill1Index),
				abilitiesData);
			skill2 = new AbilityCaster(avatar, abilitiesGroup.GetAbility(gameplayPlayerData.Skill2Index),
				abilitiesData);
			skill3 = new AbilityCaster(avatar, abilitiesGroup.GetAbility(gameplayPlayerData.Skill3Index),
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