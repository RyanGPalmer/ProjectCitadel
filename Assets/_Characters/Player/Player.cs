using System.Collections.Generic;
using UnityEngine;
using RPG.CameraUI;
using RPG.Weapons;
using RPG.Core;

namespace RPG.Characters
{
	public class Player : MonoBehaviour
	{
		const string DEFAULT_ATTACK_ANIMATION = "DEFAULT_ATTACK";

		[SerializeField] float targetableDistance = 20f;
		[SerializeField] float attackDamage = 20f;
		[SerializeField] Weapon currentWeapon;
		[SerializeField] AnimatorOverrideController animatorOverride;

		FreeLookCam cam;
		GameObject enemyTarget;
		int enemyTargetIndex;
		List<GameObject> targetableEnemies = new List<GameObject>();
		float lastAttackTime = -100f;

		void Start()
		{
			FindCamera();
			OverrideAnimator();
			SpawnCurrentWeapon();
		}

		void OverrideAnimator()
		{
			var animator = GetComponent<Animator>();
			animator.runtimeAnimatorController = animatorOverride;
			animatorOverride[DEFAULT_ATTACK_ANIMATION] = currentWeapon.GetAttackAnimation();
		}

		void FindCamera()
		{
			cam = Camera.main.GetComponentInParent<FreeLookCam>();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				CycleEnemyTargets();
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ClearTarget();
			}

			if (Input.GetButtonDown("Fire1"))
			{
				Attack();
			}
		}

		void SpawnCurrentWeapon()
		{
			var mainHand = GetComponentInChildren<MainHand>().transform;
			var spawnedWeapon = Instantiate(currentWeapon.GetPrefab(), mainHand);
			spawnedWeapon.transform.localPosition = currentWeapon.gripTransform.localPosition;
			spawnedWeapon.transform.localRotation = currentWeapon.gripTransform.localRotation;
		}

		void Attack()
		{
			// Ensure cooldown has elapsed
			if (Time.time - lastAttackTime < currentWeapon.GetAttackCooldown())
				return;

			GetComponent<Animator>().SetTrigger("Attack");

			if (enemyTarget)
			{
				var distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
				if (distance <= currentWeapon.GetAttackRange())
				{
					var enemyHealth = enemyTarget.GetComponent<Health>();
					if (enemyHealth)
					{
						(enemyHealth as IDamageable).TakeDamage(attackDamage);
					}
				}
			}

			lastAttackTime = Time.time;
		}

		void CycleEnemyTargets()
		{
			RefreshTargetableList();

			// Only executes if we have enemies in range
			if (targetableEnemies.Count > 0)
			{
				enemyTargetIndex++;

				// Check if index is out of range after incrementing
				if (enemyTargetIndex >= targetableEnemies.Count)
				{
					// If so, reset to beginning
					enemyTargetIndex = 0;
				}

				enemyTarget = targetableEnemies[enemyTargetIndex];
				cam.SetLookTarget(enemyTarget.transform);
			}
		}

		void RefreshTargetableList()
		{
			targetableEnemies = new List<GameObject>(); // Clear list
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

			foreach (GameObject enemy in enemies)
			{
				float distance = Vector3.Distance(transform.position, enemy.transform.position);
				if (distance <= targetableDistance)
				{
					targetableEnemies.Add(enemy);
				}
			}

			// Remove currently targeted enemy from list to ensure target will be different
			if (targetableEnemies.Count > 1 && enemyTarget != null)
			{
				targetableEnemies.Remove(enemyTarget);
			}
		}

		void ClearTarget()
		{
			enemyTarget = null;
			enemyTargetIndex = 0;
			cam.SetLookTarget(null);
		}
	}
}