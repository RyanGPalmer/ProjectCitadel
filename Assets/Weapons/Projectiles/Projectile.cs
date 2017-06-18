using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float lifeSpan = 10f;
	[SerializeField] float speed = 10f;

	GameObject owner = null;
	SphereCollider col = null;
	Vector3 prevPos = Vector3.zero;
	int ignoredLayer = 0;
	float damage = 10f;
	bool isPiercing = false;

	// Use this for initialization
	void Awake()
	{
		col = GetComponent<SphereCollider>();
	}

	void Start()
	{
		Invoke("Expire", lifeSpan);
		prevPos = transform.position;
		print("Ignoring layer: " + ignoredLayer);
	}

	public void SetDamage(float amount)
	{
		damage = amount;
	}

	public void SetOwner(GameObject newOwner)
	{
		owner = newOwner;
		ignoredLayer = 1 << owner.layer;
	}

	public float GetSpeed()
	{
		return speed;
	}

	void LateUpdate()
	{
		// Here we will raycast the distance traveled since the last frame. This works better than continuous dynamic
		// collision for projectiles, but may fail to detect collision with another very fast-moving object.
		Vector3 direction = (prevPos - transform.position).normalized;
		float distance = Vector3.Distance(prevPos, transform.position);
		var hits = Physics.RaycastAll(transform.position, direction, distance, ~ignoredLayer);

		foreach (RaycastHit hit in hits)
		{
			var damageable = hit.collider.GetComponent(typeof(IDamageable));
			if (damageable)
			{
				(damageable as IDamageable).TakeDamage(damage);
			}

			// TODO Make sure it's registering a hit with the FIRST object it passes through before exiting
			if (!isPiercing)
				Destroy(gameObject);
		}

		prevPos = transform.position;
	}

	// Called when projectile has exceeded its life span
	void Expire()
	{
		Destroy(gameObject);
	}
}
