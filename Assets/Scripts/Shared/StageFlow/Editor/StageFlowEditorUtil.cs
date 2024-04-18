using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace MagicCombat.Shared.StageFlow.Editor
{
	internal class StageFlowEditorUtil
	{
		private static StageFlowEditorUtil instance;

		public static StageFlowEditorUtil Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new StageFlowEditorUtil
					{
						list = new StageOrderedList()
					};
				}

				return instance;
			}
		}
		
		public StageFlowWindow window;
		public OdinMenuTree tree;
		public StageOrderedList list;

		public StageOrderedList EditorList
		{
			get
			{
				list.Refresh();
				return list;
			}
		}

		public void Refresh()
		{
			list.Refresh();
			
			window?.ForceMenuTreeRebuild();
		}
	}
}