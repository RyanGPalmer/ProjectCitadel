using UnityEngine;

namespace RPG.Weapons
{
	[CreateAssetMenu(menuName = "RPG/Weapon")]
	public class Weapon : ScriptableObject
	{
		public Transform gripTransform;

		[SerializeField] GameObject weaponPrefab;
		[SerializeField] AnimationClip attackAnimation;

		public GameObject GetPrefab()
		{
			return weaponPrefab;
		}

		public AnimationClip GetAttackAnimation()
		{
			// Clear animation events in case asset packs include them, potentially causing bugs
			attackAnimation.events = new AnimationEvent[0];

			return attackAnimation;
		}
	}
}