using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Shoot : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public UIManager uiManager;

    [HideInInspector]
    public ShootStats stats;
    public Projectile projectile;

	public Transform shootPosition;
    public Camera fpsCam;
    private WaitForSeconds duration = new WaitForSeconds (0.07f);
    private Vector3 rayOrigin;

    public void Trigger ()
	{
        Vector3 playerVelocity = gameObject.GetComponent<Rigidbody>().velocity;

        Quaternion rot = new Quaternion();
        rot.eulerAngles = fpsCam.transform.rotation.eulerAngles;
        Projectile clone = GameObject.Instantiate(projectile, shootPosition.position, rot) as Projectile;
        clone.gameManager = gameManager;
        clone.uiManager = uiManager;
        clone.stats = stats;

        Vector3 bullet_speed = fpsCam.transform.forward.normalized * stats.speed;
        clone.GetComponent<Rigidbody>().velocity = bullet_speed + playerVelocity;

        clone.Initialize();
	}
}
