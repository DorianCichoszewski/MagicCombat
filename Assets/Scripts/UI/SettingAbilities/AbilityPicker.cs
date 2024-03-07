using System;
using System.Collections.Generic;
using Gameplay.Abilities;
using MagicCombat.Gameplay.Abilities;
using TMPro;
using UnityEngine;

namespace MagicCombat.UI.SettingAbilities
{
	public class AbilityPicker : MonoBehaviour
	{
		[SerializeField]
		private TMP_Dropdown dropdown;

		[SerializeField]
		private AbilitiesGroup abilitiesGroup;

		public void Init(Action<BaseAbility> onAbilityChanged, BaseAbility startAbility = null)
		{
			dropdown.options = AbilitiesOptions(abilitiesGroup);
			dropdown.SetValueWithoutNotify(abilitiesGroup.Abilities.IndexOf(startAbility));

			dropdown.onValueChanged.AddListener(index => onAbilityChanged(abilitiesGroup.Abilities[index]));
		}

		private List<TMP_Dropdown.OptionData> AbilitiesOptions(AbilitiesGroup group)
		{
			List<TMP_Dropdown.OptionData> options = new();

			foreach (var ability in group.Abilities)
			{
				options.Add(new TMP_Dropdown.OptionData(ability.name, ability.SelectionIcon, Color.white));
			}

			return options;
		}
	}
}