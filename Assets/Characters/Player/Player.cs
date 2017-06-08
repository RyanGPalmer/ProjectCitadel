using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class Player : MonoBehaviour
{
	[SerializeField] float targetableDistance = 20f;
	[SerializeField] float attackRange = 2f;
	[SerializeField] float attackDamage = 20f;
	[SerializeField] float attackCoolDown = 1f;

	FreeLookCam cam;
	GameObject enemyTarget;
	int enemyTargetIndex = 0;
	List<GameObject> targetableEnemies = new List<GameObject>();
	float lastAttackTime = 0f;

	private void Awake()
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

	void Attack()
	{
		// Ensure cooldown has elapsed
		if (Time.time - lastAttackTime < attackCoolDown)
			return;

		if (enemyTarget)
		{
			var distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
			if (distance <= attackRange)
			{
				var enemyHealth = enemyTarget.GetComponent<Health>();
				if (enemyHealth)
				{
					(enemyHealth as IDamageable).TakeDamage(attackDamage);
					lastAttackTime = Time.time;
				}
			}
		}
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
