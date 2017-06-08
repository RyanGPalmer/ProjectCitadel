using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] float lifeSpan = 10f;

	public float speed = 10f;

	SphereCollider col = null;
	float damage = 10f;
	GameObject ignoredObject = null;
	Vector3 prevPos = Vector3.zero;

	// Use this for initialization
	void Awake()
	{
		col = GetComponent<SphereCollider>();
	}

	void Start()
	{
		Invoke("Expire", lifeSpan);
		prevPos = transform.position;
	}

	public void SetDamage(float amount)
	{
		damage = amount;
	}

	public void IgnoreObject(GameObject obj)
	{
		ignoredObject = obj;
	}

	void LateUpdate()
	{
		// Here we will raycast the distance traveled since the last frame. This works better than continuous dynamic
		// collision for projectiles, but may fail to detect collision with another very fast-moving object.
		Vector3 direction = (prevPos - transform.position).normalized;
		float distance = Vector3.Distance(prevPos, transform.position);
		var hits = Physics.RaycastAll(transform.position, direction, distance);

		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject == ignoredObject)
				continue;

			var damageable = hit.collider.GetComponent(typeof(IDamageable));
			if (damageable)
			{
				(damageable as IDamageable).TakeDamage(damage);
			}
		}
		
		prevPos = transform.position;
	}

	// Called when projectile has exceeded its life span
	void Expire()
	{
		Destroy(gameObject);
	}
}
