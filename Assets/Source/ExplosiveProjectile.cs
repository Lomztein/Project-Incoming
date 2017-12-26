using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour {

    public Projectile baseProjectile;

    public float explosionRange;
    public float explosionDamage;

    public AnimationCurve damageFalloff;

    void OnHit (RaycastHit hit) {
        Collider [ ] nearby = Physics.OverlapSphere (hit.point, explosionRange, baseProjectile.hittableLayer);
        Debug.Log (nearby.Length);
        for (int i = 0; i < nearby.Length; i++) {
            GameObject hitObj = nearby [ i ].transform.root.gameObject;
            IDamageable damageable = hitObj.GetComponent<IDamageable> ();
            if (damageable != null)
                damageable.Damage (new Damage (explosionDamage * damageFalloff.Evaluate (Vector3.Distance (hit.point, hitObj.transform.position) / explosionRange), 0f));

            Rigidbody rigidbody = hitObj.GetComponentInChildren<Rigidbody> ();
            if (rigidbody) {
                rigidbody.AddExplosionForce (explosionDamage, hit.point, explosionRange);
            }
        }
    }

}
