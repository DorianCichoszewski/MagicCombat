using System;
using Shared.Data;
using Shared.GameState;
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

		protected UserId UserId;

		private Action onReady;
		private StaticUserDataGroup staticUserDataGroup;

		public bool IsReady => readyToggle?.isOn ?? true;
		public PlayerProvider PlayerProvider { get; private set; }

		public StaticUserData StaticUserData => staticUserDataGroup.Get(UserId);

		public void Init(UserId id, Action onReady)
		{
			UserId = id;
			this.onReady = onReady;

			PlayerProvider = ScriptableLocator.Get<PlayerProvider>();
			staticUserDataGroup = ScriptableLocator.Get<StaticUserDataGroup>();

			var user = PlayerProvider.GetUser(id);
			user.SetUIFocus(gameObject, firstElement.gameObject, readyToggle.gameObject);

			readyToggle?.onValueChanged.RemoveListener(PressedReady);
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