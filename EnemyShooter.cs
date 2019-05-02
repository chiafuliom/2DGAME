using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour {

	[SerializeField]
	private GameObject bullet;
	// Use this for initialization
	void Start () {
		//this calls our attack function to start shooting the bullets right from the start of the game.
		StartCoroutine (Attack ());
	}

	IEnumerator Attack(){
		yield return new WaitForSeconds (Random.Range (1, 3));
		//this attaches the bullet to the position of the shooter.

		Instantiate (bullet, transform.position, Quaternion.identity);
		//this begins the coroutine
		StartCoroutine (Attack ());
	}

}