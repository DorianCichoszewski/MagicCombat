using MagicCombat.Gameplay.Abilities;
using MagicCombat.Gameplay.Abilities.Base;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic Combat/StartData")]
public class StartAbilitiesData : ScriptableObject
{
	[SerializeField]
	private AbilitiesGroup abilitiesGroup;

	[Header("Start Abilities")]
	[SerializeField]
	private BaseAbility startUtility;

	[SerializeField]
	private BaseAbility startSkill1;
	[SerializeField]
	private BaseAbility startSkill2;
	[SerializeField]
	private BaseAbility startSkill3;

	public AbilitiesGroup AbilitiesGroup => abilitiesGroup;

	public int StartUtilityIndex => abilitiesGroup.Abilities.IndexOf(startSkill1);
	public int StartSkill1Index => abilitiesGroup.Abilities.IndexOf(startSkill1);
	public int StartSkill2Index => abilitiesGroup.Abilities.IndexOf(startSkill1);
	public int StartSkill3Index => abilitiesGroup.Abilities.IndexOf(startSkill1);

	private void OnValidate()
	{
		//CheckAbility(ref startUtility);
		CheckAbility(ref startSkill1);
		CheckAbility(ref startSkill2);
		CheckAbility(ref startSkill3);

		void CheckAbility(ref BaseAbility ability)
		{
			if (ability != null &&!abilitiesGroup.Abilities.Contains(ability))
			{
				ability = null;
			}
		}
	}
}