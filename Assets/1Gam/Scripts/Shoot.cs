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
    private Vector3 rayOrigin;

    public void Trigger ()
	{
        Quaternion rot = new Quaternion();
        rot.eulerAngles = fpsCam.transform.rotation.eulerAngles;
        Projectile clone = GameObject.Instantiate(projectile, shootPosition.position, rot) as Projectile;
        clone.gameManager = gameManager;
        clone.uiManager = uiManager;
        clone.stats = stats;

        clone.GetComponent<Rigidbody>().velocity = fpsCam.transform.forward.normalized * stats.speed;

        clone.Initialize();
	}
}
