using UnityEngine;
using System.Collections;

public abstract class BaseUI : MonoBehaviour {

    public UIManager uiManager;

    public abstract void ShootReady();
    public abstract void Cooldown(float cooldownTimeLeft, float baseCooldown);
    public abstract void ButtonTrigger();
}
