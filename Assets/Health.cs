using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
	[SerializeField] float maxHP = 100f;
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
	}
}
