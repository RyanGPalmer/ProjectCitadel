using UnityEngine;

namespace RPG.Weapons
{
	[CreateAssetMenu(menuName = "RPG/Weapon")]
	public class Weapon : ScriptableObject
	{
		public Transform gripTransform;

		[SerializeField] GameObject weaponPrefab;
		[SerializeField] AnimationClip attackAnimation;
		[SerializeField] float attackCooldown = 2f;
		[SerializeField] float attackRange = 1.5f;

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

		public float GetAttackCooldown()
		{
			return attackCooldown;
		}

		public float GetAttackRange()
		{
			return attackRange;
		}
	}
}