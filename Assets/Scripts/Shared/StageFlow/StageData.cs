using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using MagicCombat.Shared.StageFlow.Editor;
#endif

namespace MagicCombat.Shared.StageFlow
{
	[CreateAssetMenu(menuName = "Magic Combat/Stage", fileName = "Stage")]
	public class StageData : ScriptableObject
	{
		public const string StagesPath = "Assets/Scriptables/Stages/";

		[Space]
		[SerializeField]
		private SceneReference sceneToLoad = new(-1);

		[SerializeField]
		[HideInInspector]
		private StageData parentStage;

		[SerializeReference]
		private IStageController controller;

		public bool HasScene => sceneToLoad.SceneIndex > -1;
		public int SceneIndex => sceneToLoad.SceneIndex;
		public StageData ParentStage => parentStage;
		public IStageController Controller => controller;

		public float Order { get; set; } = 999;

		public string FullName
		{
			get
			{
				string parentName = ParentStage == null ? "" : ParentStage.FullName + "/";
				return parentName + name;
			}
		}

		public bool IsEmptyStage => Controller == null && sceneToLoad.SceneIndex < 0;

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
			StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void SetParent(StageData newParent)
		{
			if (this == newParent) return;

			parentStage = newParent;
			Order = 999;
			StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void MoveUp()
		{
			Order -= 1.5f;
			StageFlowEditorUtil.Instance.Refresh();
		}

		[Button]
		public void MoveDown()
		{
			Order += 1.5f;
			StageFlowEditorUtil.Instance.Refresh();
		}
#endif
	}
}