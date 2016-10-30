using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Shoot))]
[RequireComponent(typeof(BaseUI))]
[RequireComponent(typeof(BaseControl))]
[RequireComponent(typeof(AudioSource))]
public class ShootCooldown : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;

    public float sBaseCooldown = 3;
	//	private GameObject weaponHolder;

	//Cooldown variables
	//	private Image shootIcon;
	private AudioSource shootAudio;
    private Shoot shoot;
    private BaseUI pawnUI;
    private BaseControl pawnControl;

    private float nextReadyTime;
	private float cooldownTimeLeft;


	void Awake ()
	{
		Initialize ();
	}

	public void Initialize ()
	{
		shoot = gameObject.GetComponent<Shoot> ();
		shootAudio = GetComponent <AudioSource> ();
        pawnUI = GetComponent<BaseUI>();
        pawnControl = GetComponent<BaseControl>();

        pawnUI.ShootReady();
	}

	void FixedUpdate ()
	{
        if (gameManager.pause)
            return;
        if (Time.time > nextReadyTime) {
            pawnUI.ShootReady ();
			if (pawnControl.shootRequested()) {
				ButtonTrigger ();
			}
		} else {
			Cooldown ();
		}
	}

	public void Cooldown ()
	{
        if (gameManager.pause)
            return;
        cooldownTimeLeft -= Time.deltaTime;
        pawnUI.Cooldown(cooldownTimeLeft, sBaseCooldown);
    }

    public void ButtonTrigger ()
	{
        if (gameManager.pause)
            return;
        nextReadyTime = Time.time + sBaseCooldown;
		cooldownTimeLeft = sBaseCooldown;

        pawnUI.ButtonTrigger();

        shootAudio.clip = gameManager.gameStats.shootSound;
		shootAudio.Play ();

		shoot.Trigger ();
	}

}
