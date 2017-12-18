using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingWeapon : ChargingWeapon {

    public Transform rotatingBarrel;
    public float rotationSpeed;

    public override void FixedUpdate () {
        base.FixedUpdate ();
        rotatingBarrel.Rotate (new Vector3 (0f, 0f, rotationSpeed * chargePitch.Evaluate (charge / chargeTreshold) * Time.fixedDeltaTime), Space.Self);
    }

}
