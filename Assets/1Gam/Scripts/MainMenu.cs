using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject howTo;
    public GameObject credits;

    public void Play()
    {
        SceneManager.LoadScene("arena");
    }
    public void HowTo()
    {
        mainMenu.SetActive(false);
        howTo.SetActive(true);
    }
    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
    public void Return()
    {
        mainMenu.SetActive(true);
        howTo.SetActive(false);
        credits.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
