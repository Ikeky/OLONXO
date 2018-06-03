using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerAI : MonoBehaviour {
	NavMeshAgent nav;
	public Transform[] waypoints;

	int setWaypoint = 0;
    float speed = 0.1f;
    void Start () {
		nav = GetComponent<NavMeshAgent> ();
	}
	void FixedUpdate () {

    }
}
