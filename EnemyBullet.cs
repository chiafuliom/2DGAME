using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

	//This searches for a collision between our bullet and our player.  if the palyer collides with the bullet both the bullet and the player disappear
	void OnCollisionEnter2D(Collision2D target) {
		if (target.gameObject.tag == "Player") { 
			Destroy (gameObject);
		}
		//this checks to see if the bullet touches the ground, then unloads the bullet if it touches ground.
		if (target.gameObject.tag == "Ground") { 
			Destroy (gameObject);
		}
	}
}


