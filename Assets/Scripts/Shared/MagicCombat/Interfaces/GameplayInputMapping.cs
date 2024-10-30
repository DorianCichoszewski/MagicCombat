using System;
using UnityEngine;

namespace Shared.Interfaces
{
	public class GameplayInputMapping
	{
		public event Action<Vector2> OnMove;
		public event Action<Vector2> OnRotate;

		public event Action OnUtility;
		public event Action OnSkill1;
		public event Action OnSkill2;
		public event Action OnSkill3;

		private Transform originTransform;
		private bool mouseRotation;

		public bool MouseRotation
		{
			get => mouseRotation;
			set => mouseRotation = value;
		}
		
		public Transform OriginTransform
		{
			get => originTransform;
			set => originTransform = value;
		}

		public GameplayInputMapping(bool useMouseRotation)
		{
			mouseRotation = useMouseRotation;
		}

		public void Clear()
		{
			OnMove = null;
			OnRotate = null;
			OnUtility = null;
			OnSkill1 = null;
			OnSkill2 = null;
			OnSkill3 = null;
		}

		public void Move(Vector2 obj)
		{
			OnMove?.Invoke(obj);
		}

		public void Rotate(Vector2 obj)
		{
			OnRotate?.Invoke(obj);
		}

		public void CastUtility()
		{
			OnUtility?.Invoke();
		}

		public void CastSkill1()
		{
			OnSkill1?.Invoke();
		}

		public void CastSkill2()
		{
			OnSkill2?.Invoke();
		}

		public void CastSkill3()
		{
			OnSkill3?.Invoke();
		}
	}
}