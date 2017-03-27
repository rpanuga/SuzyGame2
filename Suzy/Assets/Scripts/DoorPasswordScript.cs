using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPasswordScript : MonoBehaviour {

	// From the inspector, add the player GameObject on this script's field
	public GameObject Camera;
	public GameObject Player;


	//Password Vars
	public string password = "4321";
	bool displayPasswordScreen = false;
	public string passwordToEdit = string.Empty;

	//UI Vars
	public int interactionDistance;
	public GameObject InteractionText;
	public string description;





	// Use this for initialization
	void Start () {
		Camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {

			//WHEN PLAYER LOOKS AT PASSWORD, 
			if (Camera.GetComponent<PlayerRaycasting> ().whatIHit.collider.name == "DoorPassword"
				&& Camera.GetComponent<PlayerRaycasting> ().whatIHit.distance <= interactionDistance) {
				//Open the KEYPAD UI
				if (!displayPasswordScreen) {
					displayPasswordScreen = true;
				}
				//Check if number is correct - Fade to black + win, ELSE close UI and play WRONG noise!



			}
		}


		//HIGHLIGHTING OBJECTS WHEN LOOKED AT SCRIPT
		if (Camera.GetComponent<PlayerRaycasting> ().didIHitAnything ()) {
			if (Camera.GetComponent<PlayerRaycasting> ().whatIHit.collider.name == "DoorPassword"
				&& Camera.GetComponent<PlayerRaycasting> ().whatIHit.distance <= interactionDistance) {


				//HIGHLIGHT OBJECT
				GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Self-Illumin/Outlined Diffuse");

				//Interact with Text Script
				InteractionText.SetActive (true);
				InteractionText.GetComponent<InteractText> ().setText (description);

			} else {
				//UNHIGHLIGHT OBJECT
				GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Diffuse");
				displayPasswordScreen = false;

				if (InteractionText.GetComponent<InteractText> ().displayText.text == description)
					InteractionText.GetComponent<InteractText> ().setText ("");
			}
		} else {
		}
	}
	void OnGUI ()
	{
		if (displayPasswordScreen) {
			GUI.Label (new Rect (300, 275, 100, 30), "Password");

			passwordToEdit = GUI.PasswordField (new Rect (300, 300, 200, 20), passwordToEdit, "*" [0], 4);

			if (Input.GetKeyDown ("return")) {
				if (passwordToEdit == password) {
					//YOU IN BOY YOU IN
					Debug.Log ("YOU GOT IN!");
				}
			}
		}
	}
}
	