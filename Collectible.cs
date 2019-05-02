using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//check to make sure the door instance is present and if so create a starting value of how many collectibles there are
		if(Door.instance != null){
			Door.instance.collectiblesCount++;
		}
	}

	/*This continuously checks for collision with the player and if it occurs, the collectible is unloaded and 
    DecrementCollectibles function is run */
	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Player") {
			Destroy (gameObject);
			if (Door.instance != null) {
				Door.instance.DecrementCollectibles ();
			}
		}
	}
}

