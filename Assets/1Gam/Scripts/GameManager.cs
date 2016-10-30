using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum GameState
    {
        INGAME = 0,
        VICTORY = 1,
        DEFEAT = 2,
    }
	public UIManager uiManager;

    public GameStats gameStats;
    public PawnStats[] pawnStats;
    public ShootStats[] shootStats;
    public GameObject player;
    public GameObject ai;

    public Pawn playerPawn { get; private set; }
    public Pawn aiPawn { get; private set; }

    public bool pause { get; private set; }
    public GameState gameState { get; private set; }
    private float startGameTime;
    public float gameDuration { get; private set; }

    void Start()
    {
        uiManager.gameManager = this;
        uiManager.Initlialize();
        gameState = GameState.INGAME;
        Random.InitState((int)Time.time);

        SpawnPlayer(0);
        SetPause(false);
        startGameTime = Time.time;
        gameDuration = 0;

        uiManager.playerHud.life[1].color = playerPawn.stats.pawnColor;
        uiManager.aiHud.life[1].color = aiPawn.stats.pawnColor;
        DisplayHealth();
    }


    void Update()
    {
        if (Input.GetButtonDown("Pause"))
            SetPause(!pause);
    }
    void FixedUpdate()
    {
        gameDuration = Time.time - startGameTime;
        uiManager.timerText.text = uiManager.GetFormattedTime();
    }

    public void SpawnPlayer(int choice)
    {
        PawnStats selectedStats = pawnStats[choice];
        ShootStats selectedShoot = shootStats[choice];

        playerPawn = InitPawn (player, selectedStats, selectedShoot);

        SpawnAI(1, player);
    }
    public void SpawnAI(int choice, GameObject player)
    {
        PawnStats selectedStats = pawnStats[choice];
        ShootStats selectedShoot = shootStats[choice];

        aiPawn = InitPawn(ai, selectedStats, selectedShoot);
    }

    private Pawn InitPawn(GameObject pawnObject, PawnStats stats, ShootStats shootStats)
    {
        Pawn pawn = pawnObject.GetComponent<Pawn>();
        pawn.gameManager = this;
        pawn.stats = stats;
        pawn.pawnRenderer.material.color = stats.pawnColor;
        ShootCooldown shootCooldown = pawnObject.GetComponent<ShootCooldown>();
        shootCooldown.gameManager = this;
        Shoot shoot = pawnObject.GetComponent<Shoot>();
        shoot.gameManager = this;
        shoot.uiManager = uiManager;
        shoot.stats = shootStats;
        BaseUI pawnUI = pawnObject.GetComponent<BaseUI>();
        pawnUI.uiManager = uiManager;
        BaseControl pawnControl = pawnObject.GetComponent<BaseControl>();
        pawnControl.gameManager = this;
        return pawn;
    }

    public void CheckEndGame()
    {
        if (aiPawn.deathCount == gameStats.deathCount)
        {
            gameState = GameState.VICTORY;
            playerPawn.pawnAudio.clip = gameStats.victorySound;
        }
        else if (playerPawn.deathCount == gameStats.deathCount)
        {
            gameState = GameState.DEFEAT;
            playerPawn.pawnAudio.clip = gameStats.defeatSound;
        }
        else
        {
            return;
        }
        playerPawn.pawnAudio.Play();
        SetPause(true);
    }
    public void DisplayHealth()
    {
        uiManager.playerHud.life[1].fillAmount = (float)playerPawn.health / (float)playerPawn.stats.startingHp;
        uiManager.playerHud.deathCount.text = "Lives " + (gameStats.deathCount -  playerPawn.deathCount);
        uiManager.aiHud.life[1].fillAmount = (float)aiPawn.health / (float)aiPawn.stats.startingHp;
        uiManager.aiHud.deathCount.text = "Lives " + (gameStats.deathCount - aiPawn.deathCount);
    }

    public void SetPause(bool p)
    {
        if (p == false && gameState != GameManager.GameState.INGAME)
            return;

        //uiManager.debugText.text = "PAUSE " + p;
        pause = p;

        Object[] objects = FindObjectsOfType(typeof(GameObject));
        if (pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            foreach (GameObject go in objects)
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Time.timeScale = 1;
            foreach (GameObject go in objects)
                go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }
    }

    //Ugly function, but used only as debug tool
    public void Cheat()
    {
        string command = uiManager.cheatcode.text;
        if (command.CompareTo("reload") == 0)
        {
            SetPause(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        else if (command.CompareTo("killplayer") == 0)
        {
            playerPawn.Damage(1000);
        }
        else if (command.CompareTo("killai") == 0)
        {
            aiPawn.Damage(1000);
        }
        else if (command.CompareTo("victory") == 0)
        {
            SetPause(false);
            while (gameState != GameState.VICTORY)
                aiPawn.Reset();
            return;
        }
        else if (command.CompareTo("defeat") == 0)
        {
            SetPause(false);
            while (gameState != GameState.DEFEAT)
                playerPawn.Reset();
            return;
        }
        else
        {
            return;
        }

        SetPause(false);
    }
}
