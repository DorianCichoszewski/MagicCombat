using System.Collections.Generic;
using Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Shared.StageFlow
{
	[CreateAssetMenu(menuName = "Single/Startup/Stages Manager", fileName = "Stages Manager")]
	public class StagesManager : StartupScriptable
	{
		public const string PlayerPrefsKey = "StageToLoad";

		[SerializeField]
		[Required]
		private AssetLabelReference stagesLabel;

		[ShowInInspector]
		[ReadOnly]
		[HideLabel]
		private StageOrderedList stages;
		
		private StageData currentStage;

		public StageOrderedList Stages => stages;
		public StageData CurrentStage => currentStage;

		public override void GameStart()
		{
			currentStage = null;
			
			SetupStages();

#if UNITY_EDITOR
			
			if (PlayerPrefs.HasKey(PlayerPrefsKey))
			{
				SceneManager.LoadScene(0, LoadSceneMode.Single);
				
				int stageKey = PlayerPrefs.GetInt(PlayerPrefsKey);
				foreach (var stage in stages)
				{
					if (stage.Key == stageKey)
					{
						GoToStage(stage);

						PlayerPrefs.DeleteKey(PlayerPrefsKey);
						PlayerPrefs.Save();
						return;
					}
				}
			}
			else
			{
				return;
			}
#endif
			RunStageDirectly(stages[0]);
		}

		// Load Stages scriptable data - should be small
		public void SetupStages()
		{
			var unorderedStages = Addressables.LoadAssetsAsync<StageData>(stagesLabel, _ => { })
				.WaitForCompletion();
			stages = new StageOrderedList(unorderedStages);
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

			RunStageDirectly(nextStage);
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

			if (currentStage != null)
			{
				// Run target scene directly if 
				int targetIndex = stages.IndexOf(intermediateStages.Peek());
				if (commonParent != null && targetIndex < currentStageIndex)
				{
					RunStageDirectly(targetStage);
					return;
				}
			}

			// Run intermediate stages and skip required ones
			while (intermediateStages.Count > 0)
			{
				currentStage = stages.GetNextScene(currentStage);
				if (currentStage == intermediateStages.Peek())
				{
					if (intermediateStages.Count == 1)
						RunStageDirectly(currentStage);
					else
						EnterStage(currentStage);

					intermediateStages.Pop();
				}

				// Skip intermediate stages if they aren't root stages
				else if (currentStage.ParentStage == intermediateStages.Peek().ParentStage &&
						 intermediateStages.Peek().ParentStage != null)
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
				currentStage.Controller.Return();
			}
		}

		private void RunStageDirectly(StageData stage)
		{
			currentStage = stage;

			if (stage.IsEmptyStage)
			{
				NextStage();
				return;
			}

			if (stage.HasScene)
			{
				var loadMode = stage.HasRootScene ? LoadSceneMode.Single : LoadSceneMode.Additive;
				stage.SceneReference.LoadScene(() => stage.Controller?.Run(), loadMode);
			}
			else
			{
				stage.Controller?.Run();
			}
		}
		
		private void EnterStage(StageData stage)
		{
			stage.Controller?.Enter();
		}

		private void SkipStage(StageData stage)
		{
			stage.Controller?.Skip();
		}

		private void ExitStage(StageData stage)
		{
			if (stage.HasScene && !stage.HasRootScene) stage.SceneReference.UnloadScene();

			stage.Controller?.Exit();
		}
	}
}