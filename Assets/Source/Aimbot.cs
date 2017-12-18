using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimbot : MonoBehaviour {

    private TargetFinder targetFinder = new TargetFinder();
    public Transform target;
    public LayerMask layer;
    public float range = 9001f;

    public static bool isPresent = false;

    private void Awake() {
        isPresent = true;
    }

    void FixedUpdate () {
        if (target) {
            foreach (Emplacement empl in Emplacement.allEmplacements) {
                if (empl.turret) {
                    empl.turret.Aim (target.position);
                    empl.turret.Fire ();
                }
            }
        }else{
            target = targetFinder.FindTarget (transform.position, range, layer);
        }
	    
	}
}
