using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float lifeSpan = 10f;

	public float speed = 10f;

	SphereCollider col = null;
	float damage = 10f;

	// Use this for initialization
	void Awake()
	{
		col = GetComponent<SphereCollider>();
	}

	void Start()
	{
		Invoke("Expire", lifeSpan);
	}

	public void SetDamage(float amount)
	{
		damage = amount;
	}

	void OnTriggerEnter(Collider collider)
	{
		var damageableComponent = collider.GetComponent(typeof(IDamageable)); // Not casting here because GameObject is a nullable type which allows for the conditional statement below
		if (damageableComponent)
		{
			(damageableComponent as IDamageable).TakeDamage(damage); // Here we cast
		}
	}

	// Called when projectile has exceeded its life span
	void Expire()
	{
		Destroy(gameObject);
	}
}
