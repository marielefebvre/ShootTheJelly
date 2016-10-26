using UnityEngine;
using System.Collections;

public class PauseMenu : AMenu
{
    public void Resume()
    {
        gameManager.SetPause(false);
    }

}
