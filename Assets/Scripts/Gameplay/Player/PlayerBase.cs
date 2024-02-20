using System.Collections.Generic;
using Gameplay.Player.Ability;
using Gameplay.Player.Basic;
using UnityEngine;

namespace Gameplay.Player
{
	public class PlayerBase : MonoBehaviour
	{
		[SerializeField]
		private MovementController movement;

		[SerializeField]
		private SkinController skin;

		[HideInInspector]
		public StartData.PlayerInit data;

		[Space]
		public AbilityCaster utility;
		public AbilityCaster skill1;
		public AbilityCaster skill2;
		public AbilityCaster skill3;

		public MovementController MovementController => movement;

		public void SetData(StartData.PlayerInit initData)
		{
			data = initData;
			skin.SetSkin(data);
			transform.position = data.spawnPos;
		}
	}
}