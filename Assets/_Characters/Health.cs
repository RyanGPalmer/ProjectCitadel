using UnityEngine;
using RPG.Core;

namespace RPG.Characters
{
	public class Health : MonoBehaviour, IDamageable
	{
		[SerializeField] float maxHP = 100f;
		[SerializeField] bool destroyOnDeath = true;

		float currentHP = 0f;

		public float percentage
		{
			get { return currentHP / maxHP; }
		}

		private void Awake()
		{
			currentHP = maxHP;
		}

		public void TakeDamage(float amount)
		{
			currentHP = Mathf.Clamp(currentHP - amount, 0f, maxHP);

			if (currentHP <= 0)
			{
				Die();
			}
		}

		void Die()
		{
			if (destroyOnDeath)
			{
				Destroy(gameObject);
			}
		}
	}
}