using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : Projectile {

    public int bounceCount;

    public override void Hit(RaycastHit hit) {
        base.Hit (hit);
        if (bounceCount > 0) {
            transform.position = hit.point;
            bounceCount--;
            StartCoroutine (PauseOneFrame (hit.normal));
        } else {
            Destroy (gameObject);
        }
    }

    private IEnumerator PauseOneFrame (Vector3 normal) {
        Vector3 newVector = Vector3.Reflect (directionVector, normal);
        directionVector = Vector3.zero;
        yield return new WaitForFixedUpdate ();
        directionVector = newVector;
    }
}
