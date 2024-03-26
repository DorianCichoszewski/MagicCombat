using System;
using UnityEngine;

namespace MagicCombat.Shared.Interfaces
{
	public interface IGameplayInputController
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnRotate;
		
		public event Action OnUtility;
		public event Action OnSkill1;
		public event Action OnSkill2;
		public event Action OnSkill3;
	}
}