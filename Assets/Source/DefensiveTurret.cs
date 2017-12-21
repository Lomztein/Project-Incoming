using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveTurret : Turret, IPlaceable, ISellable {

    public static List<DefensiveTurret> allDefensiveTurrets = new List<DefensiveTurret> ();
    public static int defensiveTurretCapacity = 2;
    public static int defensiveTurretMaxCapacity = 8;

    public TargetFinder targetFinder = new TargetFinder ();
    public Transform target;

    public LayerMask targetLayerMask;
    public float range;

    public long cost;

    public static bool AtCapacity () {
        return allDefensiveTurrets.Count >= defensiveTurretCapacity;
    }

    public int GetSellValue() {
        return Mathf.RoundToInt (cost * 0.75f);
    }

    public bool PickUp() {
        allDefensiveTurrets.Add (this);
        enabled = false;
        return true;
    }

    public bool Place() {
        enabled = true;
        return true;
    }

    public void Sell() {
        Destroy (gameObject);
        allDefensiveTurrets.Remove (this);
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
