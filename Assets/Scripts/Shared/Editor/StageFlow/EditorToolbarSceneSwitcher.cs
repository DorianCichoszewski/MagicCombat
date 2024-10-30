using System;
using Shared.StageFlow;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace Shared.Editor.StageFlow
{
	[InitializeOnLoad]
    internal static class EditorToolbarSceneSwitcher
    {
        private const string LastSelectedBuildSceneKey = "LastSelectedBuildSceneKey";
        private const string LastSelectedBuildSceneName = "LastSelectedBuildSceneName";
        
        static EditorToolbarSceneSwitcher()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
        }

        static void OnRightToolbarGUI()
        {
            //using (new GUIEnabled(!EditorApplication.isPlaying))
            if (!EditorApplication.isPlaying)
            {
                ShowPlayButton();
                ShowMenu();
            }
            else
            {
                var content = new GUIContent($"Current: {StageFlowEditorUtil.Instance.CurrentStage?.FullNamePrintable}");
                GUI.Label(GetThickArea(GUILayoutUtility.GetRect(content, new GUIStyle())), content);
            }
        }

        static void ShowPlayButton()
        {
            string scenePath = SceneManager.GetActiveScene().path;
            if (!CanRunScenePyPath(scenePath, out _)) return;
            
            var tex = EditorGUIUtility.IconContent(@"PlayButton").image;
            
            using (new GUIColor(new Color(0.34f, 1f, 0.64f)))
            {
                var content = new GUIContent(tex, "Start from current Scene");
                var guiStyle = (GUIStyle) "Command";

                var rect = GetThickArea(GUILayoutUtility.GetRect(content, guiStyle));
                if (GUI.Button(rect, content, guiStyle))
                {
                    RunScenePyPath(scenePath);
                }
            }
        }

        static void ShowMenu()
        {
            bool hasLastSelectedName = EditorPrefs.HasKey(LastSelectedBuildSceneName);
            var lastSelectedName = EditorPrefs.GetString(LastSelectedBuildSceneName);
            var content = new GUIContent(hasLastSelectedName ? lastSelectedName : "Run scene from build list");
            var guiStyle = (GUIStyle) "DropDown";
            var rect = GetThickArea(GUILayoutUtility.GetRect(content, guiStyle));

            var stages = StageFlowEditorUtil.Instance.EditorList;

            if (GUI.Button(rect, content, guiStyle))
            {
                if (EditorPrefs.HasKey(LastSelectedBuildSceneKey) && Event.current.button == 0)
                {
                    int lastSelectedStageKey = EditorPrefs.GetInt(LastSelectedBuildSceneKey);
                    StageFlowEditorUtil.Instance.RunStage(lastSelectedStageKey);
                }
                else
                {
                    GenericMenu menu = new GenericMenu();
                    
                    foreach (var stage in stages)
                    {
                        menu.AddItem(new GUIContent(stage.FullName.Replace("/", "\\")), false, StartScene, stage);
                    }
                    menu.DropDown(new Rect(Event.current.mousePosition, Vector2.zero));
                }
            }
        }
        
        static bool CanRunScenePyPath(string path, out StageData stageFromPath)
        {
            string guid = AssetDatabase.AssetPathToGUID(path);
            var stages = StageFlowEditorUtil.Instance.EditorList;
            foreach (var stage in stages)
            {
                if (!stage.HasScene) continue;
                
                if (stage.SceneGUID == guid)
                {
                    stageFromPath = stage;
                    return true;
                }
            }

            stageFromPath = null;
            return false;
        }

        static void RunScenePyPath(string path)
        {
            if (CanRunScenePyPath(path, out var stage))
            {
                StageFlowEditorUtil.Instance.RunStage(stage.Key);
            }
            else
            {
                Debug.LogError("No stage with this scene found");
            }
        }

        static void StartScene(object stageObj)
        {
            StageData stage = stageObj as StageData;
            EditorPrefs.SetInt(LastSelectedBuildSceneKey, stage.Key);
            EditorPrefs.SetString(LastSelectedBuildSceneName, stage.FullNamePrintable);
            StageFlowEditorUtil.Instance.RunStage(stage.Key);
        }
        
        private static Rect GetThickArea(Rect pos)
        {
            return new Rect(pos.x, 0f, Mathf.Min(pos.width, 180), 24f);
        }
    }
    
    public class GUIEnabled : IDisposable
    {
        private readonly bool prevValue;
        
        public GUIEnabled(bool enabled)
        {
            prevValue = GUI.enabled;
            GUI.enabled = enabled;
        }

        public void Dispose()
        {
            GUI.enabled = prevValue;
        }
    }
    
    public class GUIColor : IDisposable
    {
        private readonly Color prevColor;
        
        public GUIColor(Color newColor)
        {
            prevColor = GUI.color;
            GUI.color = newColor;
        }

        public void Dispose()
        {
            GUI.color = prevColor;
        }
    }
}