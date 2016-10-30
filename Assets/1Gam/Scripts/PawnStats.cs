using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Stats/PawnStats")]
public class PawnStats : ScriptableObject
{
	public string pawnName = "White";
	public Color pawnColor = Color.white;

	//public float speed = 3;
	public int startingHp = 10;

    public float respawnDelay = 1.0f;
}