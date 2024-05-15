using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace MagicCombat.Shared.StageFlow.Editor
{
	[InitializeOnLoad]
    internal static class EditorToolbarSceneSwitcher
    {
        static EditorToolbarSceneSwitcher()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
        }

        static void OnRightToolbarGUI()
        {
            using (new GUIEnabled(EditorApplication.isPlaying == false))
            {
                ShowPlayButton();
                ShowMenu();
            }
        }

        static void ShowPlayButton()
        {
            var tex = EditorGUIUtility.IconContent(@"PlayButton").image;
            
            using (new GUIColor(new Color(0.34f, 1f, 0.64f)))
            {
                var content = new GUIContent(tex, "Start from current Scene");
                var guiStyle = (GUIStyle) "Command";

                var rect = GetThickArea(GUILayoutUtility.GetRect(content, guiStyle));
                if (GUI.Button(rect, content, guiStyle))
                {
                    GetScenePyPath(SceneManager.GetActiveScene().path);
                }
            }
        }

        static void ShowMenu()
        {
            var content = new GUIContent("Build Scenes ");
            var guiStyle = (GUIStyle) "DropDown";
            var rect = GetThickArea(GUILayoutUtility.GetRect(content, guiStyle));

            var stages = StageFlowEditorUtil.Instance.EditorList;

            if (GUI.Button(rect, content, guiStyle))
            {
                GenericMenu menu = new GenericMenu();

                foreach (var stage in stages)
                {
                    menu.AddItem(new GUIContent(stage.FullName), false, StartScene, stage);
                }
                
                menu.DropDown(new Rect(Event.current.mousePosition, new Vector2(150, stages.Count * 20)));
            }
        }

        static void GetScenePyPath(string path)
        {
            string guid = AssetDatabase.AssetPathToGUID(path);
            var stages = StageFlowEditorUtil.Instance.EditorList;
            foreach (var stage in stages)
            {
                if (stage.SceneReference.SceneGUID == guid)
                {
                    StageFlowEditorUtil.Instance.RunStage(stage);
                    return;
                }
            }

            Debug.LogError("No stage with this scene found");
        }

        static void StartScene(object stageObj)
        {
            StageFlowEditorUtil.Instance.RunStage(stageObj as StageData);
        }
        
        private static Rect GetThickArea(Rect pos)
        {
            return new Rect(pos.x, 0f, Mathf.Min(pos.width, 150), 24f);
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