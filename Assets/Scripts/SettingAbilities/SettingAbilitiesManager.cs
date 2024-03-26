using System.Collections.Generic;
using MagicCombat.Gameplay;
using MagicCombat.Player;
using MagicCombat.Shared.GameState;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.SettingAbilities
{
	public class SettingAbilitiesManager : BaseManager
	{
		[Required]
		public StartAbilitiesData startAbilities;
		[ReadOnly]
		public List<PlayerData> playersData;
		
		public void Init(List<PlayerData> playersData)
		{
			this.playersData = playersData;
			foreach (var data in playersData)
			{
				data.gameplay ??= new GameplayPlayerData(startAbilities);
			}
		}
		
		public void Next()
		{
			Debug.Log("Finished abilities setup");
			sharedScriptable.ProjectScenes.GoToGameplay();
		}
	}
}