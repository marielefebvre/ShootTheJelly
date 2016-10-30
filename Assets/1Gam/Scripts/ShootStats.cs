using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Stats/ShootStats")]
public class ShootStats : ScriptableObject
{
	//COMMON
	public string sName = "Shoot";
	public string pawnName = "White";
	public Color pawnColor = Color.white;
	public Sprite sprite;
	//public AudioClip sound;
	public float baseCoolDown = 1f;
	public int damage = 1;

	//RAYCAST
	public float speed = 16f;
	public float lifetime = 1f;

	//PROJECTILE
	public float range = 50f;
	public float force = 100f;
}
