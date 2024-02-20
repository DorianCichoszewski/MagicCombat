using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace SurvivorSandbox.Editor
{
	[CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
	[CanEditMultipleObjects]
	public class DefaultEditor : UnityEditor.Editor
	{
		public override VisualElement CreateInspectorGUI()
		{
			var container = new VisualElement();

			var iterator = serializedObject.GetIterator();
			if (iterator.NextVisible(true))
				do
				{
					var propertyField = new PropertyField(iterator.Copy())
						{ name = "PropertyField:" + iterator.propertyPath };

					if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
						propertyField.SetEnabled(false);

					container.Add(propertyField);
				} while (iterator.NextVisible(false));

			return container;
		}
	}
}