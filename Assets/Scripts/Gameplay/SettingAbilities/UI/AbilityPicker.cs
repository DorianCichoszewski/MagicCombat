using System;
using System.Collections.Generic;
using MagicCombat.Gameplay.Abilities;
using TMPro;
using UnityEngine;

namespace MagicCombat.SettingAbilities.UI
{
	public class AbilityPicker : MonoBehaviour
	{
		[SerializeField]
		private TMP_Dropdown dropdown;

		public void Init(AbilitiesCollection abilitiesCollection, Action<int> onAbilityChanged, int startAbility = -1)
		{
			dropdown.options = AbilitiesOptions(abilitiesCollection);
			dropdown.SetValueWithoutNotify(startAbility);

			dropdown.onValueChanged.AddListener(index => onAbilityChanged(index));
		}

		private List<TMP_Dropdown.OptionData> AbilitiesOptions(AbilitiesCollection collection)
		{
			List<TMP_Dropdown.OptionData> options = new();

			foreach (var ability in collection.Abilities)
			{
				options.Add(new TMP_Dropdown.OptionData(ability.name, ability.DefaultIcon, Color.white));
			}

			return options;
		}
	}
}