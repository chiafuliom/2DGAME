using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
	public void Lvl1(){
		SceneManager.LoadScene("Level 1");
	}
	public void Lvl2(){
		SceneManager.LoadScene("Level 2");
	}

	public void BackToMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}

