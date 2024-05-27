using Shared.Services;
using UnityEditor;
using UnityEngine;

namespace Shared.Editor.DragAndDrop
{
	public static class DragAndDropHandler
	{
		[InitializeOnLoadMethod]
		static void OnLoad()
		{
			UnityEditor.DragAndDrop.AddDropHandler(OnProjectBrowserDrop);
		}

		private static DragAndDropVisualMode OnProjectBrowserDrop(int dragInstanceId, string dropUponPath, bool perform)
		{
			var scriptable = UnityEditor.DragAndDrop.objectReferences[0] as ScriptableObject;
			if (scriptable == null)
				return DragAndDropVisualMode.Move;

			var asset = AssetDatabase.LoadMainAssetAtPath(dropUponPath);
			if (asset is not ScriptableService dropUponService)
				return DragAndDropVisualMode.Move;
			
			if (perform)
			{
				AssetDatabase.AddObjectToAsset(scriptable, dropUponService);
				AssetDatabase.SaveAssets();
			}

			return DragAndDropVisualMode.Link;
		}
	}
}