using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Utility;

[RequireComponent(typeof(ObjectResetter))]
public class Pawn : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public UIManager uiManager;
    [HideInInspector]
    public PawnStats stats;

    public Renderer pawnRenderer;
    public GameObject ink;

    public int health { get; private set; }
    public int deathCount { get; private set; }
    private float nextDamageTime;

    private ObjectResetter resetter;
    public AudioSource pawnAudio { get; private set; }

    void Start()
	{
        resetter = gameObject.GetComponent<ObjectResetter>();
        pawnAudio = GetComponent<AudioSource>();

        health = stats.startingHp;
        deathCount = 0;
	}

	public void Damage (int damage)
	{
		health -= damage;
        pawnAudio.clip = gameManager.gameStats.damageSound;
        pawnAudio.Play();
		if (health <= 0) {
            //SpreadInk();
            resetter.DelayedReset(stats.respawnDelay);
        }
        gameManager.DisplayHealth();
    }

    public void Reset()
    {
        deathCount++;
        pawnAudio.clip = gameManager.gameStats.deathSound;
        pawnAudio.Play();
        health = stats.startingHp;
        gameManager.DisplayHealth();
        gameManager.CheckEndGame();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.pause)
            return;

        Collider other = collision.collider;
        if (other.gameObject.CompareTag("Pawn"))
        {
            Damage(1);
        }
    }

    //public void SpreadInk()
    //{
    //    Vector3 pos = transform.position;
    //    pos.y = 0;
    //    GameObject clone = GameObject.Instantiate(ink, pos, transform.rotation) as GameObject;
    //    clone.GetComponent<Renderer>().material.color = stats.pawnColor;
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ink")
    //        && other.gameObject.GetComponent<Renderer>().material.color != stats.pawnColor)
    //    {
    //        nextDamageTime = Time.time + gameManager.gameStats.poisonCooldown;
    //        Damage(1);
    //    }
    //}
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ink")
    //        && other.gameObject.GetComponent<Renderer>().material.color != stats.pawnColor)
    //    {
    //        if (Time.time > nextDamageTime)
    //        {
    //            nextDamageTime = Time.time + gameManager.gameStats.poisonCooldown;
    //            Damage(1);
    //        }
    //    }
    //}
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Ink")
    //        && other.gameObject.GetComponent<Renderer>().material.color != stats.pawnColor)
    //    {
    //        nextDamageTime = -1;
    //    }
    //}

}
