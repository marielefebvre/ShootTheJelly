using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;

	public Text debugText;
    public InputField cheatcode;

    public GameObject inGameHud;
    public PauseMenu pauseMenu;
    public EndMenu endMenu;

    public Image shootIcon;
	public Image darkMask;
	public Text cooldownText;

    public Text timerText;

    public struct PawnHud
    {
        public Text deathCount;
        public Image[] life;///0 icon, 1 mask
    }

    public PawnHud playerHud;
    public PawnHud aiHud;
    public GameObject playerHudPanel;
    public GameObject aiHudPanel;

    private static UIManager instance = null;

	protected UIManager ()
	{
    }

	public static UIManager Instance {
		get {
			return instance;
		}
	}

	private void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
		}
		instance = this;
    }
    public void Initlialize()
    {
        playerHud.deathCount = playerHudPanel.GetComponentInChildren<Text>();
        playerHud.life = playerHudPanel.GetComponentsInChildren<Image>();
        aiHud.deathCount = aiHudPanel.GetComponentInChildren<Text>();
        aiHud.life = aiHudPanel.GetComponentsInChildren<Image>();
        pauseMenu.gameManager = gameManager;
        endMenu.gameManager = gameManager;
    }

    public void OnPauseGame()
    {
        inGameHud.SetActive(false);
        switch (gameManager.gameState)
        {
            case GameManager.GameState.INGAME:
                pauseMenu.gameObject.SetActive(true);
                pauseMenu.SetTitle("Pause");
                return;
            case GameManager.GameState.VICTORY:
                endMenu.SetTitle("Victory");
                break;
            case GameManager.GameState.DEFEAT:
                endMenu.SetTitle("Defeat");
                break;
            default:
                return;
        }
        endMenu.gameObject.SetActive(true);
        endMenu.timerText.text = GetFormattedTime();
        endMenu.playerDeathCount.text = gameManager.playerPawn.stats.pawnName + " kills " + gameManager.aiPawn.deathCount.ToString();
        endMenu.aiDeathCount.text = gameManager.aiPawn.stats.pawnName + " kills " + gameManager.playerPawn.deathCount.ToString();
    }
    public void OnResumeGame()
    {
        if (gameManager.gameState != GameManager.GameState.INGAME)
            Debug.LogWarning("Resume ui while not ingame");
        inGameHud.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
    }
    public string GetFormattedTime()
    {
        return ((int)gameManager.gameDuration / 60).ToString("00") + ":" + ((int)gameManager.gameDuration % 60).ToString("00");
    }
}
