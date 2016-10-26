using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndMenu : AMenu
{

	//public EventSystem eventSystem;
	public Text score;
	//public Button restart;
	//public Button next;

	//public void Next() {
	//	PlayerPrefs.SetInt("level", PlayerPrefs.GetInt ("level") + 1);
	//	Application.LoadLevel(Application.loadedLevelName);
	//}

	void OnEnable() {
		Debug.Log("End menu");

		//eventSystem.firstSelectedGameObject = restart.gameObject;
		//EventSystem.current.SetSelectedGameObject(null);
		//next.interactable = (PlayerPrefs.GetInt ("level") != 3);
		
		//if (gameManager.gameState == GameManager.GameState.VICTORY) {
		//	title.text = "You won !";
		//	score.text = "You reached the end in " + gameManager.score + " hours !";
		//} else if (gameManager.gameState == GameManager.GameState.DEATH) {
		//	title.text = "You lost :(";
		//	score.text = "The sun got you after " + gameManager.score + " hours.";
		//}
		//EventSystem.current.SetSelectedGameObject(null);
	}
}
