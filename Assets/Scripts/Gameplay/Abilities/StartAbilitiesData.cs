using MagicCombat.Gameplay.Abilities;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic Combat/One Time/StartData")]
public class StartAbilitiesData : ScriptableObject
{
	[SerializeField]
	[Required]
	private AssetReferenceAbility startUtility;

	[SerializeField]
	[Required]
	private AssetReferenceAbility startSkill1;

	[SerializeField]
	[Required]
	private AssetReferenceAbility startSkill2;

	[SerializeField]
	[Required]
	private AssetReferenceAbility startSkill3;

	public string StartUtility => startUtility.AssetGUID;
	public string StartSkill1 => startSkill1.AssetGUID;
	public string StartSkill2 => startSkill2.AssetGUID;
	public string StartSkill3 => startSkill3.AssetGUID;
}