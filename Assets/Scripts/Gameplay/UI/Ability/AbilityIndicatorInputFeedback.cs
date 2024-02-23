using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.Ability
{
	public class AbilityIndicatorInputFeedback : MonoBehaviour
	{
		[SerializeField]
		private Image ringImage;

		[SerializeField]
		private Transform shakeableElement;

		[Space]
		[SerializeField]
		private float succesfulTweenDuration = 0.5f;

		[SerializeField]
		private Color succesfulTweenColor = Color.yellow;

		[Space]
		[SerializeField]
		private float failedTweenDuration = 0.5f;

		[SerializeField]
		private float failedTweenStrength = 1f;

		[SerializeField]
		private int failedTweenVibrato = 10;

		private Tween failedPerformTween;

		private Tween successfulPerformTween;

		private void OnValidate()
		{
			SetupTweens();
		}

		public void SetupTweens()
		{
			successfulPerformTween = ringImage.DOColor(succesfulTweenColor, succesfulTweenDuration)
				.OnComplete(() => ringImage.color = Color.white).SetLoops(2, LoopType.Yoyo).SetAutoKill(false).Pause()
				.Done();
			failedPerformTween = shakeableElement
				.DOShakePosition(failedTweenDuration, new Vector3(failedTweenStrength, 0), failedTweenVibrato)
				.SetAutoKill(false).Pause().Done();
		}

		public void AbilityPerformed()
		{
			successfulPerformTween.Restart();
		}

		public void AbilityFailedPerformed()
		{
			failedPerformTween.Restart();
		}
	}
}