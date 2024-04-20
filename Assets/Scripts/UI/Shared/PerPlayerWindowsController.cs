using System;
using System.Collections.Generic;
using System.Linq;
using MagicCombat.Shared.Data;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.UI.Shared
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class PerPlayerWindowsController
    {
        [SerializeField]
        [Required]
        private PerPlayerWindow windowPrefab;

        [SerializeField]
        [Required]
        private Transform windowsParent;

        private List<PerPlayerWindow> createdWindows;
        private Action onReady;

        public void CreateWindows(SharedScriptable shared, Action onAllWindowsReady)
        {
            onReady = onAllWindowsReady;

            foreach (var id in shared.PlayerProvider.PlayersEnumerator)
            {
                var newWindow = GameObject.Instantiate(windowPrefab, windowsParent);
                createdWindows.Add(newWindow);
                newWindow.Init(shared, id, CheckWindowsReady);
            }
        }

        public PerPlayerWindow GetWindow(PlayerId id)
        {
            return createdWindows.FirstOrDefault(x => x.PlayerId == id);
        }

        private void CheckWindowsReady()
        {
            foreach (var window in createdWindows)
            {
                if (!window.IsReady)
                    return;
            }

            onReady();
        }
    }
}