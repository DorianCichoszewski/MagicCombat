using System;
using Shared.Data;
using Shared.GameState;
using Shared.Interfaces;
using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace MagicCombat.UI.Shared
{
	public class PerPlayerWindow : MonoBehaviour
	{
		[SerializeField]
		[Required]
		private Selectable firstElement;

		[SerializeField]
		private ReadyToggle readyToggle;

		protected PlayerId playerId;

		private Action onReady;
		private PlayerProvider playerProvider;

		public bool IsReady => readyToggle?.isOn ?? true;
		public PlayerProvider PlayerProvider => playerProvider;

		public void Init(PlayerId id, Action onReady)
		{
			playerId = id;
			this.onReady = onReady;
			
			playerProvider = ScriptableLocator.Get<PlayerProvider>();

			var inputController = PlayerProvider.InputController(id);
			inputController.SetUIFocus(gameObject, firstElement.gameObject, readyToggle.gameObject);

			readyToggle?.onValueChanged.AddListener(PressedReady);
			
			OnInit();
		}

		protected virtual void OnInit() { }

		protected virtual bool CanBeReady()
		{
			return true;
		}

		private void PressedReady(bool newValue)
		{
			if (!newValue) return;

			if (!CanBeReady())
			{
				readyToggle.SetIsOnWithoutNotify(false);
				return;
			}

			onReady();
		}
	}
}