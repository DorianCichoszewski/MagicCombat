using System;
using System.Collections.Generic;
using MagicCombat.Gameplay;
using MagicCombat.SettingAbilities;
using MagicCombat.SettingPlayer;
using MagicCombat.Shared.GameState;
using MagicCombat.Shared.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Directors
{
	public class DirectorsManager : MonoBehaviour, IEssentialScript
	{
		private Dictionary<Type, IDirector> directors;

		private GlobalState globalState;
		private IDirector currentDirector;

		[ShowInInspector]
		private string CurrentDirectorName => currentDirector?.GetType().Name ?? "";
		
		public void Init(GlobalState globalState)
		{
			this.globalState = globalState;
			directors = new()
			{
				{ typeof(SettingPlayerManager), new SettingPlayerDirector() },
				{ typeof(SettingAbilitiesManager), new SettingAbilitiesDirector() },
				{ typeof(GameplayManager), new GameplayDirector() }
			};

			globalState.OnNewRegisteredManager += NewManager;
		}

		public void Validate()
		{
		}

		private void NewManager(BaseManager manager)
		{
			directors[manager.GetType()].Run(manager, globalState);
		}
	}
}