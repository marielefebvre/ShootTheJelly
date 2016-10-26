using UnityEngine;
using System.Collections;

public class PlayerUI : BaseUI {

    public override void ShootReady()
    {
        uiManager.darkMask.enabled = false;
        uiManager.cooldownText.enabled = false;
    }
    public override void Cooldown(float cooldownTimeLeft, float baseCooldown)
    {
        uiManager.cooldownText.text = Mathf.Round(cooldownTimeLeft).ToString();
        uiManager.darkMask.fillAmount = cooldownTimeLeft / baseCooldown;
    }
    public override void ButtonTrigger()
    {
        uiManager.darkMask.enabled = true;
        uiManager.cooldownText.enabled = true;
    }
}
