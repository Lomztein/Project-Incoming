using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour, IPlaceable {

    public float explosionRange;
    public float explosionDamage;
    public float explosionArmorPenetration;
    public float explosionForce;
    public AnimationCurve explosionDamageFalloff;
    public GameObject explosionParticle;

    public LayerMask targetLayer;

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
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
        return true;
    }

    void OnTriggerEnter (Collider other) {
        Explode ();
    }

    public void Explode () {
        Collider [ ] nearby = Physics.OverlapSphere (transform.position, explosionRange, targetLayer);
        foreach (Collider col in nearby) {
            float damage = explosionDamage * explosionDamageFalloff.Evaluate (Vector3.Distance (transform.position, col.transform.position) / explosionRange);

            IDamageable damageable = col.transform.root.GetComponentInChildren<IDamageable> ();
            Rigidbody rigidbody = col.transform.GetComponentInParent<Rigidbody> ();

            if (damageable != null) {
                damageable.Damage (new Damage (damage, explosionArmorPenetration));
            }

            if (rigidbody) {
                rigidbody.AddExplosionForce (explosionForce, transform.position, explosionRange);
            }
        }

        Destroy (Instantiate (explosionParticle, transform.position, transform.rotation), 5f);
        Destroy (gameObject);
    }
}
