using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class AMenu : MonoBehaviour {
    [HideInInspector]
    public GameManager gameManager;
    public Text title;

    public void SetTitle(string t)
    {
        title.text = t;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        gameManager.ReturnMainMenu();
    }
}
