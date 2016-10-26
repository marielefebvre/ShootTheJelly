using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Stats/GameStats")]
public class GameStats : ScriptableObject
{
    //not used for now
	public float durationMax = 600;//seconds

    public int deathCount = 5;

    public float respawnDelay = 1.0f;
    public float poisonCooldown = 0.2f;

    public AudioClip shootSound;
    public AudioClip damageSound;
    public AudioClip deathSound;
    public AudioClip defeatSound;
    public AudioClip victorySound;
}
