﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPasswordScript : MonoBehaviour {


	//Password Vars
	public string password = "4321";
	public string enteredPassword = "";
	public GameObject player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//Check if unlocked
		if (password == enteredPassword && player.GetComponent<keyScript>().playerHasKey == false) {
			//GIVE PLAYER THE KEY
			player.GetComponent<keyScript>().playerHasKey = true; //RUN FOR IT CHARLIE
			this.GetComponent<AudioSource>().Play();
		}
		else if (enteredPassword.Length == 4) {
			enteredPassword = "";
			//PLAY YOU SUCK SOUND?
		}
	}

	public void enterNumber(int newNum) {
		enteredPassword = enteredPassword + newNum.ToString();
		return;
	}
}
	