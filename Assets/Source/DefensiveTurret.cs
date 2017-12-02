using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTurret : Turret, IPlaceable {

    public TargetFinder targetFinder = new TargetFinder ();
    public Transform target;

    public LayerMask targetLayerMask;
    public float range;

        public void Place() {
        enabled = true;
    }

    public void ToPosition(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void ToTransform(Transform transform) {
    }

    // Update is called once per frame
    new void FixedUpdate () {
        base.FixedUpdate ();

        if (!target) {
            target = targetFinder.FindTarget (transform.position, range, targetLayerMask);
            SetIdle ();
        } else {
            Aim (target.position);
            Fire ();

            if (Vector3.Distance (transform.position, target.position) > range)
                target = null;
        }
	}
}
