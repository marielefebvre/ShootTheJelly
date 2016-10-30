using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public UIManager uiManager;

    [HideInInspector]
    public ShootStats stats;
    [HideInInspector]
    public Vector3 previousVelocity;

    private float death;

	public void Initialize ()
	{
        gameObject.GetComponent<Renderer>().material.color = stats.pawnColor;
        death = Time.time + stats.lifetime;
	}

	void FixedUpdate ()
	{
        if (gameManager.pause)
            return;
        if (Time.time >= death) {
			Destroy (gameObject);
		}
	}

    public void OnPauseGame()
    {
        previousVelocity = gameObject.GetComponent<Rigidbody>().velocity;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
    }
    public void OnResumeGame()
    {
        gameObject.GetComponent<Rigidbody>().velocity = previousVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager.pause)
            return;
        Collider other = collision.collider;
        if (other.gameObject.CompareTag("Pawn"))
        {
            Pawn pawn = other.gameObject.GetComponent<Pawn>();
            if (pawn.stats.pawnColor != stats.pawnColor)
            {
                pawn.Damage(stats.damage);
            }
        }
        Destroy (gameObject);
    }
}
