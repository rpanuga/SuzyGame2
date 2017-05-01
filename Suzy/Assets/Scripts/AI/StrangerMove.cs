using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class StrangerMove : MonoBehaviour {

	public Transform goal;

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {

	}
	public void StartMoving() {
		this.GetComponent<NavMeshAgent>().destination = goal.position;
	}
}
