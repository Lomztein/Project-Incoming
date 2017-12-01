using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder {

    public Transform FindTarget(Vector3 position, float range, LayerMask layer) {
        Collider [ ] nearby = Physics.OverlapSphere (position, range, layer);

        float lastValue = float.MaxValue;
        Transform pick = null;

        foreach (Collider near in nearby) {
            float value = Vector3.SqrMagnitude (near.transform.position - position);
            if (value < lastValue) {
                lastValue = value;
                pick = near.transform;
            }
        }

        return pick;
    }

    public Transform FindTarget(Ray ray, float radius, float distance, LayerMask layer) {
        RaycastHit hit;
        if (Physics.SphereCast (ray, radius, out hit, distance, layer)) {
            return hit.transform;
        }
        return null;
    }
}
