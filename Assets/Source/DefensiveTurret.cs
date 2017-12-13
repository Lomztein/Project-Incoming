﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTurret : Turret, IPlaceable {

    public TargetFinder targetFinder = new TargetFinder ();
    public Transform target;

    public LayerMask targetLayerMask;
    public float range;

    public bool PickUp() {
        enabled = false;
        return true;
    }

    public bool Place() {
        enabled = true;
        return true;
    }

    public bool ToPosition(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
        return true;
    }

    public bool ToTransform(Transform transform) {
        return false;
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
