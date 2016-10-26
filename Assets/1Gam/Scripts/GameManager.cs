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
        uiManager.debugText.text = "Start game\n";
        uiManager.Initlialize();
        gameState = GameState.INGAME;
        Random.InitState((int)Time.time);
        //mainCamera = GetComponentInParent <Camera> ();

        SpawnPlayer(0);
        SetPause(false);
        startGameTime = Time.time;
        gameDuration = 0;

        Debug.Log("Game Start");

        uiManager.playerHud.life[1].color = playerPawn.stats.pawnColor;
        uiManager.aiHud.life[1].color = aiPawn.stats.pawnColor;
        DisplayHealth();
        Debug.Log("Hud done");
    }


    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log("pause");
            SetPause(!pause);
        }

        gameDuration = Time.time - startGameTime;
        uiManager.timerText.text = uiManager.GetFormattedTime();
    }
    public void SpawnPlayer(int choice)
    {
        PawnStats selectedStats = pawnStats[choice];
        ShootStats selectedShoot = shootStats[choice];

        //GameObject player = Instantiate (pawnPrefab, playerSpawnPosition, Quaternion.identity) as GameObject;
        playerPawn = InitPawn (player, selectedStats, selectedShoot);
        //PlayerController playerController = player.AddComponent<PlayerController>();
        //MouseLook mouseLook = player.AddComponent <MouseLook> ();
        //		PlayerFollow follow = .gameObject.AddComponent <PlayerFollow> ();
        //		follow.player = player;

        SpawnAI(1, player);
    }

    public void SpawnAI(int choice, GameObject player)
    {
        PawnStats selectedStats = pawnStats[choice];
        ShootStats selectedShoot = shootStats[choice];

        //GameObject ai = Instantiate (pawnPrefab, aiSpawnPosition, Quaternion.identity) as GameObject;
        //ai.transform.Rotate (0, 180, 0);
        aiPawn = InitPawn(ai, selectedStats, selectedShoot);
        //AIController aiController = ai.AddComponent<AIController>();
        //aiController.enemy = player;
    }

    private Pawn InitPawn(GameObject pawnObject, PawnStats stats, ShootStats shootStats)
    {
        uiManager.debugText.text += "Spawn " + stats.pawnName + "\n";
        Pawn pawn = pawnObject.GetComponent<Pawn>();
        pawn.gameManager = this;
        pawn.stats = stats;
        pawn.pawnRenderer.material.color = stats.pawnColor;
        //Camera pawnCamera = pawnObject.GetComponentInChildren<Camera>();
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
        uiManager.playerHud.deathCount.text = "Death " + playerPawn.deathCount;
        uiManager.aiHud.life[1].fillAmount = (float)aiPawn.health / (float)aiPawn.stats.startingHp;
        uiManager.aiHud.deathCount.text = "Death " + aiPawn.deathCount;
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
            {
                go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            Time.timeScale = 1;
            foreach (GameObject go in objects)
            {
                go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
            }
        }
        //uiManager.SetPause(p);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Ugly function, but used only as debug tool
    public void Cheat()
    {
        string command = uiManager.cheatcode.text;
        uiManager.debugText.text = "Cheat : " + command;
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
            //aiPawn.Damage(1000);
            return;
        }
        else if (command.CompareTo("defeat") == 0)
        {
            SetPause(false);
            while (gameState != GameState.DEFEAT)
                playerPawn.Reset();
            //playerPawn.Damage(1000);
            return;
        }
        else
        {
            return;
        }

        SetPause(false);
    }
}
