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
    public Vector3 initialVelocity;

    private float death;

	public void Initialize ()
	{
        gameObject.GetComponent<Renderer>().material.color = stats.pawnColor;
        death = Time.time + stats.lifetime;
	}

	void Update ()
	{
        if (gameManager.pause)
            return;
        //gameObject.GetComponent<Rigidbody>().AddForce(initialVelocity);
        if (Time.time >= death) {
			Destroy (gameObject);
		}
	}

    public void OnPauseGame()
    {
        //uiManager.debugText.text = "OnpauseGame proj ";
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
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
        //		ContactPoint contact = collision.contacts[0];
        //		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //		Vector3 pos = contact.point;
        //		Instantiate(explosionPrefab, pos, rot);
        //		Destroy(gameObject);

        Destroy (gameObject);
    }
}
