using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_script : MonoBehaviour {
	public string LoadScene;

	private string PreparingScene = "";
	public void StartScene(){
		Application.LoadLevel (LoadScene);
	}
	public void Quit(){
		Application.Quit ();
	}
}
