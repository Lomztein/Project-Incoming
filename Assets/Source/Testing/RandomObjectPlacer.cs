using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectPlacer : MonoBehaviour {

    public GameObject toPlace;
    public float placeRepeatTime;
    public float placeRadius;

	// Use this for initialization
	void Start () {
        InvokeRepeating ("Place", placeRepeatTime, placeRepeatTime);
	}

    void Place () {
        Instantiate (toPlace, transform.position + Random.insideUnitSphere * placeRadius, Quaternion.identity);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere (transform.position, placeRadius);
    }
}
