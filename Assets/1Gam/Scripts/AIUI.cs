using UnityEngine;
using System.Collections;

public class AIUI : BaseUI
{

    public override void ShootReady()
    {
        //uiManager.debugText.text = "AI shoot ready";
    }
    public override void Cooldown(float cooldownTimeLeft, float baseCooldown)
    {
        //uiManager.debugText.text = "AI cooldown";
    }
    public override void ButtonTrigger()
    {
        //uiManager.debugText.text = "AI button trigger";
    }
}
