using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour {

	public void Return() {
        SceneManager.LoadScene(0);
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update(){
		if (Input.GetButtonDown ("Quit"))
			Return ();
	}
}
