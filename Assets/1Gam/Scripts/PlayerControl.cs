using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerControl : BaseControl {

    public string shootButtonAxisName = "Fire1";

    public override bool shootRequested()
    {
        return Input.GetButton(shootButtonAxisName);
    }

    public void OnPauseGame()
    {
        gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;
    }
    public void OnResumeGame()
    {
        gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;
    }

}
