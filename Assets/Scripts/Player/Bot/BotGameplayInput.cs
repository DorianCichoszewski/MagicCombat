using System;
using Shared.Interfaces;
using UnityEngine;

namespace MagicCombat.Player.Bot
{
	public class BotGameplayInput : MonoBehaviour, IGameplayInputController
	{
#pragma warning disable 67
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnRotate;
		public event Action OnUtility;
		public event Action OnSkill1;
		public event Action OnSkill2;
		public event Action OnSkill3;
#pragma warning restore 67
		public void Clear()
		{
		}
		
		// TODO: Add bot gameplay logic
	}
}