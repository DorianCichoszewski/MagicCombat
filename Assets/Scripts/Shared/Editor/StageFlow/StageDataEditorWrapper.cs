using System.IO;
using Shared.StageFlow;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Shared.Editor.StageFlow
{
	public class StageDataEditorWrapper
	{
		[ShowInInspector]
		[InlineEditor(Expanded = true, DrawHeader = false, ObjectFieldMode = InlineEditorObjectFieldModes.Boxed)]
		private StageData stageData;
		
		public StageDataEditorWrapper(StageData stage)
		{
			stageData = stage;
		}
		
		[PropertySpace(20)]
		[Button]
		public void CreateNew(string newStageName)
		{
			string directory = Path.GetDirectoryName(AssetDatabase.GetAssetPath(stageData));
			string path = directory + newStageName + ".asset";
			if (File.Exists(path))
			{
				Debug.LogError($"Stage with name {newStageName} already exists");
				return;
			}

			var newStage = ScriptableObject.CreateInstance<StageData>();
			newStage.name = newStageName;
			newStage.SetParent(stageData);
			AssetDatabase.CreateAsset(newStage, path);
			StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void SetParent(StageData newParent)
		{
			if (stageData == newParent) return;

			stageData.SetParent(newParent);
			stageData.ChangeOrder(9999f);
			StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void MoveUp()
		{
			stageData.ChangeOrder(-1.5f);
			StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void MoveDown()
		{
			stageData.ChangeOrder(1.5f);
			StageFlowEditorUtil.Instance.Refresh();
		}
	}
}