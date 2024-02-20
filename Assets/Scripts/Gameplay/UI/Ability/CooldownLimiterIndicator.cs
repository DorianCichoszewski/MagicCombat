using System.Runtime.InteropServices.WindowsRuntime;
using Extension;
using Gameplay.Limiters;
using TMPro;
using UnityEngine;

namespace Gameplay.UI.Ability
{
	public class CooldownLimiterIndicator : MonoBehaviour
	{
		[SerializeField]
		private RectTransform background;

		[SerializeField]
		private TMP_Text text;
		
		[SerializeField]
		private bool showTime = true;
		[SerializeField]
		private bool showProgressBar = true;

		private CooldownLimiter limiter;
		
		public void AssignLimiter(CooldownLimiter limiter)
		{
			this.limiter = limiter;
			
			text.gameObject.SetActive(showTime);
			background.gameObject.SetActive(true);
		}

		private void Update()
		{
			if (limiter == null) return;
			
			background.gameObject.SetActiveCached(!limiter.CanPerform());
			if (limiter.CanPerform())
			{
				text.text = string.Empty;
				return;
			}
			
			if (showTime)
			{
				text.text = $"{limiter.RemainingTime:F1}";
			}

			if (showProgressBar)
				background.anchorMax = new Vector2(1f, limiter.RemainingPercent);

		}
	}
}