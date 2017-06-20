using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
	public class EnemyHealthBar : MonoBehaviour
	{
		RawImage healthBarRawImage = null;
		Health enemyHealth = null;

		// Use this for initialization
		void Start()
		{
			enemyHealth = GetComponentInParent<Health>(); // Different to way player's health bar finds player
			healthBarRawImage = GetComponent<RawImage>();
		}

		// Update is called once per frame
		void Update()
		{
			float xValue = -(enemyHealth.percentage / 2f) - 0.5f;
			healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
		}
	}
}