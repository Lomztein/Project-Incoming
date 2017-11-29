using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public static Map gameMap;
    public GameObject worldPlane;
    public Collider worldPlaneCollider;

    private void Awake() {
        gameMap = this;
        worldPlaneCollider = worldPlane.GetComponent<Collider> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
