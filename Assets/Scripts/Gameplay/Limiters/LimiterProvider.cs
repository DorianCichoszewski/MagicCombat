using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
#endif

namespace Gameplay.Limiters
{
	[Serializable]
	public class LimiterProvider
	{
		public const string LimiterTypeName = nameof(limiterType);
		public const string LimiterName = nameof(limiterData);

		public LimiterType limiterType;

		[SerializeReference]
		private ILimiter limiterData = new CooldownLimiter();

		public ILimiter Limiter => limiterData.Copy();

		public void Init()
		{
			limiterData = GetLimiterFromType(limiterType);
			limiterData.Reset();
		}

		public bool CanPerform()
		{
			return limiterData.CanPerform();
		}

		public bool TryToPerform()
		{
			return limiterData.TryPerform();
		}

		public void Reset()
		{
			limiterData.Reset();
		}

		public static ILimiter GetLimiterFromType(LimiterType type)
		{
			return type switch
			{
				LimiterType.None => new NoneLimiter(),
				LimiterType.Cooldown => new CooldownLimiter(),
				LimiterType.Charges => new ChargesLimiter(),
				_ => throw new ArgumentOutOfRangeException()
			};
		}
	}

	[Serializable]
	public enum LimiterType
	{
		Cooldown,
		Charges,
		None
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(LimiterProvider))]
	public class LimiterProviderPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var element = new VisualElement
			{
				name = "LimiterPropertyDrawer",
				style = { flexGrow = 1f }
			};

			var limiterTypeProperty = property.FindPropertyRelative(LimiterProvider.LimiterTypeName);
			var limiterProperty = property.FindPropertyRelative(LimiterProvider.LimiterName);

			var currentType = (LimiterType)limiterTypeProperty.enumValueIndex;

			var limiterTypeField = new EnumField(property.displayName, currentType);
			limiterTypeField.BindProperty(limiterTypeProperty);
			limiterTypeField.RegisterValueChangedCallback(x =>
			{
				if (x.newValue != null)
				{
					limiterProperty.boxedValue = LimiterProvider.GetLimiterFromType((LimiterType)x.newValue);
					property.serializedObject.ApplyModifiedProperties();
				}
			});
			element.Add(limiterTypeField);

			var limiterField = new PropertyField(limiterProperty);
			element.Add(limiterField);

			return element;
		}
	}
#endif
}