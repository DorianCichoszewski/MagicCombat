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

		private AbilitiesGroup abilitiesGroup;

		public void Init(AbilitiesGroup group, Action<int> onAbilityChanged, int startAbility = -1)
		{
			abilitiesGroup = group;
			dropdown.options = AbilitiesOptions(abilitiesGroup);
			dropdown.SetValueWithoutNotify(startAbility);

			dropdown.onValueChanged.AddListener(index => onAbilityChanged(index));
		}

		private List<TMP_Dropdown.OptionData> AbilitiesOptions(AbilitiesGroup group)
		{
			List<TMP_Dropdown.OptionData> options = new();

			foreach (var ability in group.Abilities)
			{
				options.Add(new TMP_Dropdown.OptionData(ability.name, ability.DefaultIcon, Color.white));
			}

			return options;
		}
	}
}