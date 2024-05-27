using Shared.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MagicCombat.Gameplay.Abilities
{
	[CreateAssetMenu(menuName = "Single/Data/Initial Abilities")]
	public class StartAbilitiesData : ScriptableService
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
}