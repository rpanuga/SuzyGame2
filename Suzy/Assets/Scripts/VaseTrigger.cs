using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseTrigger : MonoBehaviour {

	public GameObject vase;
	public float speed;

	// Update is called once per frame
	void Start () {
		vase.GetComponent<Rigidbody> ().velocity = vase.transform.forward * speed;
		vase.GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.Impulse);
	}
		

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			vase.gameObject.SetActive (true);

			// TODO: Destroy the vase after xxx seconds OR CHANGE IT INTO BROKEN CLASS
			Destroy(vase, 4.0f); 
			Destroy (this.gameObject, 4.0f);

		}
	}

}
