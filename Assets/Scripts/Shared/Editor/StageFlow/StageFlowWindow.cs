#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Shared.Editor.StageFlow
{
	internal class StageFlowWindow : OdinMenuEditorWindow
	{
		[MenuItem("Magic Combat/Stage Flow")]
		private static void OpenWindow()
		{
			StageFlowEditorUtil.Instance.window = GetWindow<StageFlowWindow>();
			StageFlowEditorUtil.Instance.window.Show();
		}

		public void Refresh()
		{
			if (MenuTree.Selection.Count > 0)
			{
				string selectionName = MenuTree.Selection[0].Name;
				ForceMenuTreeRebuild();
				MenuTree.Selection.Clear();
				SelectPrevious(MenuTree.MenuItems);

				bool SelectPrevious(List<OdinMenuItem> items)
				{
					foreach (var item in items)
					{
						if (item.Name == selectionName)
						{
							MenuTree.Selection.Add(item);
							return true;
						}

						if (item.ChildMenuItems != null && SelectPrevious(item.ChildMenuItems))
						{
							return true;
						}
					}

					return false;
				}
			}
			else
			{
				ForceMenuTreeRebuild();
			}
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

			tree.Selection.SelectionConfirmed += selection =>
			{
				Debug.Log("Selected " + selection[0].Value, selection[0].Value as Object);
				EditorGUIUtility.PingObject(selection[0].Value as Object);
			};

			foreach (var stage in StageFlowEditorUtil.Instance.EditorList)
			{
				tree.Add(stage.FullName, new StageDataEditorWrapper(stage));
			}

			return tree;
		}
	}
}
#endif