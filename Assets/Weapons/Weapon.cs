using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Weapon")]
public class Weapon : ScriptableObject
{
	[SerializeField] GameObject weaponPrefab;
	[SerializeField] AnimationClip attackAnimation;

	public GameObject GetPrefab()
	{
		return weaponPrefab;
	}
}
