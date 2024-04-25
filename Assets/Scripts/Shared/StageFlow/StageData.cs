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
		// For creating new Stage
		public const string StagesPath = "Assets/Scriptables/Stages/";

		[Space]
		[SerializeField]
		private SceneReference sceneReference;

		[SerializeReference]
		private IStageController controller;

		[SerializeField]
		[HideInInspector]
		private StageData parentStage;

		[SerializeField]
		[HideInInspector]
		private float order = 999;

		public bool HasScene => sceneReference.HasScene;
		internal SceneReference SceneReference => sceneReference;
		public StageData ParentStage => parentStage;

		// ReSharper disable once ConvertToAutoProperty
		public IStageController Controller => controller;

		public float Order
		{
			get => order;
			set => order = value;
		}

		public string FullName
		{
			get
			{
				string parentName = ParentStage == null ? "" : ParentStage.FullName + "/";
				return parentName + name;
			}
		}

		public bool IsEmptyStage => Controller == null && !sceneReference.HasScene;

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

					otherStage = otherStage.ParentStage;
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