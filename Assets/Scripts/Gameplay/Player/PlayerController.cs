using MagicCombat.Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Gameplay.Player
{
	public class PlayerController
	{
		public bool EnabledInput { get; set; } = true;

		private readonly PlayerAvatar player;
		private readonly IGameplayInputController input;

		public PlayerController(PlayerAvatar player, IGameplayInputController input)
		{
			this.input = input;
			this.player = player;

			input.OnMove += Move;
			input.OnRotate += Rotate;
			input.OnUtility += Utility;
			input.OnSkill1 += Skill1;
			input.OnSkill2 += Skill2;
			input.OnSkill3 += Skill3;
		}

		public void Clear()
		{
			input.Clear();
		}

		private void Move(Vector2 value)
		{
			if (!EnabledInput) return;

			player.MovementController.Move(value);
		}

		private void Rotate(Vector2 value)
		{
			if (!EnabledInput) return;

			player.MovementController.Rotate(value);
		}

		private void Utility()
		{
			if (!EnabledInput) return;
			if (!player.Alive) return;

			player.utility.TryToPerform();
		}

		private void Skill1()
		{
			if (!EnabledInput) return;
			if (!player.Alive) return;

			player.skill1.TryToPerform();
		}

		private void Skill2()
		{
			if (!EnabledInput) return;
			if (!player.Alive) return;

			player.skill2.TryToPerform();
		}

		private void Skill3()
		{
			if (!EnabledInput) return;
			if (!player.Alive) return;

			player.skill3.TryToPerform();
		}
	}
}