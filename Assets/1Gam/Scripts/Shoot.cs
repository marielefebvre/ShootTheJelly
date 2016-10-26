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

	//Raycast variables
	public Transform shootPosition;
    public Camera fpsCam;
    private WaitForSeconds duration = new WaitForSeconds (0.07f);
    private Vector3 rayOrigin;

    //TMP
    //	[HideInInspector] public int damage = 1;
    //	[HideInInspector] public float range = 50f;
    //	[HideInInspector] public float force = 100f;
    [HideInInspector] public LineRenderer line;

    void Awake ()
	{
		Initialize ();
	}

	public void Initialize ()
	{
		line = gameObject.GetComponent<LineRenderer> ();
		//fpsCam = gameObject.GetComponent<Camera> ();
	}

	void Update ()
	{
        rayOrigin = shootPosition.position;
        Debug.DrawRay(rayOrigin, fpsCam.transform.forward * stats.range, Color.blue);
        Vector3 camRayOrigin = fpsCam.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f));
        Debug.DrawRay(camRayOrigin, fpsCam.transform.forward * stats.range, Color.green);
    }

    public void Trigger ()
	{
		StartCoroutine ("ShotEffect");

        //RaycastHit hit;
        //line.SetPosition (0, shootPosition.position);
        //if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, stats.range)) {
        //	line.SetPosition (1, hit.point);

        //	//			ShootableBox box = hit.collider.gameObject.GetComponent<ShootableBox> ();
        //	//			if (box != null) {
        //	//				box.Damage (damage);
        //	//			}
        //	//			Rigidbody rb = hit.collider.GetComponent<Rigidbody> ();
        //	//			if (rb != null) {
        //	//				rb.AddForce (force * -hit.normal);
        //	//			}
        //} else {
        //	line.SetPosition (1, rayOrigin + (fpsCam.transform.forward * stats.range));
        //}
        Vector3 playerVelocity = gameObject.GetComponent<Rigidbody>().velocity;
        float projSpeed = 16.0f;

        Quaternion rot = new Quaternion();
        rot.eulerAngles = fpsCam.transform.rotation.eulerAngles;
        Projectile clone = GameObject.Instantiate(projectile, rayOrigin, rot) as Projectile;
        clone.gameManager = gameManager;
        clone.uiManager = uiManager;
        clone.stats = stats;
        //clone.initialVelocity = (fpsCam.transform.forward.normalized + fpsCam.transform.up.normalized).normalized;
        //Vector3 bullet_speed = transform.TransformDirection(new Vector3(0, 0, 4.0f));
        Vector3 bullet_speed = fpsCam.transform.forward.normalized * projSpeed;
        //clone.initialVelocity = bullet_speed;// + playerVelocity;

        clone.GetComponent<Rigidbody>().velocity = bullet_speed + playerVelocity;

        //clone.initialVelocity = fpsCam.transform.forward.normalized + playerVelocity;
        Debug.DrawRay(rayOrigin, rayOrigin + clone.initialVelocity, Color.red);
        clone.Initialize();
	}

	IEnumerator ShotEffect ()
	{
		//gunAudio.Play ();

		//line.enabled = true;
		yield return duration;
		//line.enabled = false;
	}
}
