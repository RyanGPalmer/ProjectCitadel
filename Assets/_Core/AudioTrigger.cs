using UnityEngine;

namespace RPG.Core
{
	public class AudioTrigger : MonoBehaviour
	{
		[SerializeField] AudioClip audioClip;
		[SerializeField] float triggerRadius = 4f;
		[SerializeField] bool playOnce = true;
		[SerializeField] int layerFilter = 0;

		bool hasPlayed = false;
		AudioSource audioSource;

		void Start()
		{
			InitializeAudio();
			InitializeCollider();
		}

		void InitializeAudio()
		{
			audioSource = gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.clip = audioClip;
		}

		void InitializeCollider()
		{
			var collider = gameObject.AddComponent<SphereCollider>();
			collider.isTrigger = true;
			collider.radius = triggerRadius;
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.layer == layerFilter)
			{
				RequestPlayAudioClip();
			}
		}

		void RequestPlayAudioClip()
		{
			if (hasPlayed && playOnce)
				return;

			if (!audioSource.isPlaying)
			{
				audioSource.Play();
				hasPlayed = true;
			}
		}
	}
}
