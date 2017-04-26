using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("BUMP");
		if (other.gameObject.name == "Player") {
			if (other.gameObject.GetComponent<keyScript>().playerHasKey == true) {
				//LET EM WIN
				Debug.Log("YOU WIN! OMG");
				Application.Quit();
			}
			else {
				//lol
				//Sound effect of door trying to be opened
			}
		}
	}
}
