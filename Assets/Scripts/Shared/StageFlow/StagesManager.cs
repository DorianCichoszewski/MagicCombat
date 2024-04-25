using System;
using System.Collections.Generic;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MagicCombat.Shared.StageFlow
{
	[Serializable]
	[InlineProperty]
	public class StagesManager
	{
		[SerializeField]
		[Required]
		private AssetLabelReference stagesLabel;

		[ShowInInspector]
		[InlineProperty]
		[HideLabel]
		private StageOrderedList stages;

		private SharedScriptable sharedScriptable;
		private StageData currentStage;

		internal StageOrderedList Stages => stages;

		public void Init(SharedScriptable shared)
		{
			sharedScriptable = shared;

#if UNITY_EDITOR
			UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
#endif

			SetupStages();

			RunStage(stages[0]);
		}

		// Load Stages scriptable data - should be small
		public async void SetupStages()
		{
			var unorderedStages = Addressables.LoadAssetsAsync<StageData>(stagesLabel, _ => { })
				.WaitForCompletion();
			stages = new(unorderedStages);
		}

		public void NextStage()
		{
			var nextStage = stages.GetNextScene(currentStage);

			// Close all unrelated parent stages
			while (currentStage != null && currentStage != nextStage.ParentStage)
			{
				ExitStage(currentStage);
				currentStage = currentStage.ParentStage;
			}

			RunStage(nextStage);
		}

		[Button]
		public void GoToStage(StageData targetStage)
		{
			int currentStageIndex = -1;
			StageData commonParent = null;

			if (currentStage != null)
			{
				currentStageIndex = stages.IndexOf(currentStage);

				currentStage.HasCommonParent(targetStage, out commonParent);

				// Exit to stage being child of common parent
				if (currentStage != commonParent)
					while (currentStage.ParentStage != commonParent)
					{
						ExitStage(currentStage);
						currentStage = currentStage.ParentStage;
					}
			}

			// Get all intermediate parents for target
			Stack<StageData> intermediateStages = new();
			while (targetStage != commonParent)
			{
				intermediateStages.Push(targetStage);
				targetStage = targetStage.ParentStage;
			}

			targetStage ??= intermediateStages.Pop();

			if (currentStage != null)
			{
				// Run target scene directly if 
				int targetIndex = stages.IndexOf(targetStage);
				if (commonParent != null && targetIndex < currentStageIndex)
				{
					RunStage(targetStage);
					return;
				}
			}

			// Run intermediate stages and skip required ones
			while (intermediateStages.Count > 0)
			{
				currentStage = stages.GetNextScene(currentStage);
				if (currentStage == targetStage)
				{
					RunStage(currentStage);
					targetStage = intermediateStages.Pop();
				}

				// Skip intermediate stages if they aren't root stages
				else if (currentStage.ParentStage == targetStage.ParentStage && targetStage.ParentStage != null)
				{
					SkipStage(currentStage);
				}
			}
		}

		public void CloseCurrentStage()
		{
			ExitStage(currentStage);

			if (currentStage.ParentStage != null)
			{
				currentStage = currentStage.ParentStage;
				currentStage.Controller.Return(sharedScriptable);
			}
		}


		private void RunStage(StageData stage)
		{
			currentStage = stage;

			if (stage.IsEmptyStage)
			{
				NextStage();
				return;
			}

			if (stage.HasScene)
			{
				stage.SceneReference.LoadScene(() => stage.Controller?.Run(sharedScriptable));
			}
			else
			{
				stage.Controller?.Run(sharedScriptable);
			}
		}

		private void SkipStage(StageData stage)
		{
			stage.Controller?.Skip(sharedScriptable);
		}

		private void ExitStage(StageData stage)
		{
			if (stage.HasScene)
			{
				stage.SceneReference.UnloadScene();
			}

			stage.Controller?.Exit(sharedScriptable);
		}
	}
}