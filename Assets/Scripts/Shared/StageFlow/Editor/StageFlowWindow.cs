using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace MagicCombat.Shared.StageFlow.Editor
{
	internal class StageFlowWindow : OdinMenuEditorWindow
	{
		[MenuItem("Magic Combat/Stage Flow")]
		private static void OpenWindow()
		{
			StageFlowEditorUtil.Instance.window = GetWindow<StageFlowWindow>();
			StageFlowEditorUtil.Instance.window.Show();
		}

		private void OnFocus()
		{
			StageFlowEditorUtil.Instance.window = this;
		}

		protected override OdinMenuTree BuildMenuTree()
		{
			var tree = new OdinMenuTree
			{
				Selection =
				{
					SupportsMultiSelect = false
				}
			};

			foreach (var stage in StageFlowEditorUtil.Instance.EditorList)
			{
				tree.Add(stage.FullName, stage);
			}
			
			StageFlowEditorUtil.Instance.tree = tree;
			
			return tree;
		}
	}
}