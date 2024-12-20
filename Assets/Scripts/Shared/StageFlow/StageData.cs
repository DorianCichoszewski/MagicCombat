using UnityEngine;

namespace Shared.StageFlow
{
	[CreateAssetMenu(menuName = "Magic Combat/Stage", fileName = "Stage")]
	public class StageData : ScriptableObject
	{
		[Space]
		[SerializeField]
		private SceneReference sceneReference;

		[SerializeReference]
		private StageController controller;

		[SerializeField]
		[HideInInspector]
		private StageData parentStage;

		[SerializeField]
		[HideInInspector]
		private float order = 999;

		public bool HasScene => sceneReference.HasScene;
		public int Key => GetHashCode();
		public string SceneGUID => SceneReference?.SceneGUID ?? string.Empty;
		public SceneReference SceneReference => sceneReference;
		public StageData ParentStage => parentStage;
		public StageController Controller => controller;

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
		
		public string FullNamePrintable => FullName.Replace("/", "\\");

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
				var otherStage = other.parentStage;
				while (otherStage != null)
				{
					if (thisStage == otherStage)
					{
						commonParent = thisStage;
						return true;
					}

					otherStage = otherStage.ParentStage;
				}

				thisStage = thisStage.parentStage;
			}

			commonParent = null;
			return false;
		}
		
		public void SetParent(StageData newParent)
		{
			parentStage = newParent;
		}
		
		public void ChangeOrder(float newOrder)
		{
			order += newOrder;
		}
	}
}