using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace MagicCombat.Shared.StageFlow
{
	[CreateAssetMenu(fileName = "Stage", menuName = "Magic Combat/Stage")]
	public class StageData : ScriptableObject
	{
		public const string StagesPath = "Assets/Scriptables/Stages/";
		
		private StageData parentStage;
		
		[SerializeReference]
		private IStageController controller;

		[Space]
		[SerializeField]
		private SceneReference sceneToLoad;
		
		[SerializeField]
		private GameObject objectToLoad;
		
		public StageData ParentStage => parentStage;
		public IStageController Controller => controller;
		public bool HasScene => sceneToLoad.SceneIndex > -1;
		public int SceneIndex => sceneToLoad.SceneIndex;
		public GameObject ObjectToLoad => objectToLoad;

		public float Order { get; set; } = 999;
		
		public string FullName
		{
			get
			{
				string parentName = parentStage == null ? "" : parentStage.FullName + "/";
				return parentName + name;
			}
		}

		public bool IsEmptyStage => controller == null && objectToLoad == null && sceneToLoad.SceneIndex < 0;

		public bool HasRootScene
		{
			get
			{
				if (!HasScene) return false;

				bool ret = true;
				var stage = this;
				while (stage.ParentStage != null)
				{
					stage = stage.ParentStage;
					if (stage.HasScene)
						ret = false;
				}

				return ret;
			}
		}

		public bool HasCommonParent(StageData other, out StageData commonParent)
		{
			var thisStage = this;
			while (thisStage.ParentStage != null)
			{
				var otherStage = other.ParentStage;
				while (otherStage != null)
				{
					if (thisStage == otherStage)
					{
						commonParent = thisStage;
						return true;
					}

					otherStage = other.ParentStage;
				}
			}

			commonParent = null;
			return false;
		}

#if UNITY_EDITOR
		[PropertySpace(20)]
		[Button]
		public void CreateNew(string newStageName)
		{
			string path = StagesPath + newStageName + ".asset";
			if (File.Exists(path))
			{
				Debug.LogError($"Stage with name {newStageName} already exists");
				return;
			}
			var newStage = CreateInstance<StageData>();
			newStage.name = newStageName;
			newStage.parentStage = this;
			AssetDatabase.CreateAsset(newStage, path);
		}
		
		[Button]
		public void SetParent(StageData newParent)
		{
			if (this == newParent) return;
			
			parentStage = newParent;
			Order = 999;
			Editor.StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void MoveUp()
		{
			Order -= 1.5f;
			Editor.StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void MoveDown()
		{
			Order += 1.5f;
			Editor.StageFlowEditorUtil.Instance.Refresh();
		}
#endif
	}
}