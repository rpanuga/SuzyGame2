using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour {

	public float distanceToSee;

	public RaycastHit whatIHit;

	private bool didIHit;

	void Start () {
		didIHit = false;
	}
		

	// Update is called once per frame
	void Update () {
		Vector3 forward = transform.TransformDirection (Vector3.forward) * distanceToSee;

		//Debug.DrawRay (this.transform.position, (this.transform.forward * distanceToSee), Color.red);
		Debug.DrawRay(transform.position,forward, Color.green);

		//DEBUGGING TOOL
		if (Physics.Raycast(transform.position, forward, out whatIHit)) {
			//Debug.Log("I touched " + whatIHit.collider.gameObject.name + " from " + whatIHit.distance);
			didIHit = true;
		} 


	}
	public bool didIHitAnything() {
		return didIHit;
	}
}
