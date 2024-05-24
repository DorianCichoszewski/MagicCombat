using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Shared.StageFlow
{
	[Serializable]
	public class StageOrderedList : IEnumerable<StageData>
	{
		[ReadOnly]
		[ShowInInspector]
		private List<StageData> orderedStages;

		public StageData this[int index] => orderedStages[index];

		public int Count => orderedStages.Count;

		public int IndexOf(StageData stage)
		{
			return orderedStages.IndexOf(stage);
		}

		public StageOrderedList(IList<StageData> unorderedStages)
		{
			Dictionary<StageData, List<StageData>> stagesWithParent = new();
			List<StageData> baseStages = new();

			foreach (var stage in unorderedStages)
			{
				if (stage.ParentStage == null)
				{
					baseStages.Add(stage);
				}
				else
				{
					stagesWithParent.TryAdd(stage.ParentStage, new List<StageData>());
					stagesWithParent[stage.ParentStage].Add(stage);
				}
			}

			List<StageData> newOrderedStages = new();

			int baseStageCount = baseStages.Count;
			for (int i = 0; i < baseStageCount; i++)
			{
				AddStage(GetLowestStage(baseStages), i);
			}

			orderedStages = newOrderedStages;
			return;

			StageData GetLowestStage(List<StageData> stages)
			{
				float lowestOrder = float.MaxValue;
				StageData lowestStage = null;
				foreach (var stage in stages)
				{
					if (stage.Order < lowestOrder)
					{
						lowestStage = stage;
						lowestOrder = stage.Order;
					}
				}

				stages.Remove(lowestStage);

				return lowestStage;
			}

			void AddStage(StageData stage, int order)
			{
				newOrderedStages.Add(stage);
				stage.Order = order;
				if (stagesWithParent.TryGetValue(stage, out var stagesList))
				{
					int subStageCount = stagesList.Count;
					for (int i = 0; i < subStageCount; i++)
					{
						AddStage(GetLowestStage(stagesList), i);
					}
				}
			}
		}

		public StageData GetNextScene(StageData currentStage)
		{
			if (currentStage == null) return orderedStages[0];
			int currentIndex = IndexOf(currentStage);
			if (currentIndex > orderedStages.Count - 1)
				return null;

			return orderedStages[currentIndex + 1];
		}

		public IEnumerator<StageData> GetEnumerator()
		{
			return orderedStages.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}