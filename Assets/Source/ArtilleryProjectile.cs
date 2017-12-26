using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryProjectile : Projectile {

	// Update is called once per frame
	public override void FixedUpdate () {
        base.FixedUpdate ();
        directionVector += Vector3.up * (Physics.gravity.y * Time.fixedDeltaTime);
        speed = directionVector.magnitude;
	}
}
