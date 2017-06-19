using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
	[RequireComponent(typeof(RawImage))]
	public class PlayerHealthBar : MonoBehaviour
	{
		RawImage healthBarRawImage;
		Health playerHealth;

		// Use this for initialization
		void Start()
		{
			playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
			healthBarRawImage = GetComponent<RawImage>();
		}

		// Update is called once per frame
		void Update()
		{
			float xValue = -(playerHealth.percentage / 2f) - 0.5f;
			healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
		}
	}
}