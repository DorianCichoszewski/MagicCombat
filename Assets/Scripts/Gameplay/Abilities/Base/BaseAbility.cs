using System.Diagnostics;
using MagicCombat.Gameplay.Abilities.Limiters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities.Base
{
	public abstract class BaseAbility : ScriptableObject
	{
		protected const string AbilitiesPath = "Abilities/";
		protected const string UtilitiesPath = "Abilities/Utilities/";

		[SerializeField]
		[HorizontalGroup(Width = 100)]
		[PreviewField(ObjectFieldAlignment.Left, Height = 80)]
		[HideLabel]
		[OnValueChanged(nameof(SetIcon))]
		protected Sprite defaultIcon;

		[HorizontalGroup(Gap = 0)]
		[HideLabel]
		public LimiterProvider limiterProvider;

		public Sprite DefaultIcon => defaultIcon;

		public abstract void Perform(AbilityCaster caster, AbilityState state);

		[Button]
		[Conditional("UNITY_EDITOR")]
		void SetIcon()
		{
			if (defaultIcon != null)
			{
				UnityEditor.EditorGUIUtility.SetIconForObject(this, defaultIcon.texture);
				UnityEditor.EditorUtility.SetDirty(this);
			}
		}
	}
}