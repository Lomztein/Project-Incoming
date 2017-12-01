using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IFireable {

    public Vector3 directionVector;
    public Weapon weapon;

    public int amount;
    public float speed;
    public float mass;
    public float armorPenetration;
    public float inaccuracy;

    public float range = 100;

    public GameObject hitParticle;

    public void Fire(Transform muzzle, Weapon firingWeapon) {
        for (int i = 0; i < amount; i++) {
            GameObject newBullet = Instantiate (gameObject, muzzle.position, Quaternion.identity);
            Projectile projectile = newBullet.GetComponent<Projectile> ();

            float rad = inaccuracy * Mathf.Deg2Rad;

            Vector3 angled = muzzle.forward + muzzle.rotation * (Vector3.right * Mathf.Sin (Random.Range (-rad, rad)) + Vector3.up * Mathf.Sin (Random.Range (-rad, rad)));
            projectile.directionVector = angled * speed;
            projectile.weapon = firingWeapon;

            newBullet.transform.rotation = Quaternion.LookRotation (angled, Vector3.up);
            
            Destroy (newBullet, range / speed);
        }
    }

    public float GetDamage() {
        return speed * mass;
    }

    private void FixedUpdate() {
        Ray nextRay = new Ray (transform.position, directionVector * Time.fixedDeltaTime);
        RaycastHit hit;

        if (Physics.Raycast (nextRay, out hit, speed * Time.fixedDeltaTime)) {
            Hit (hit);
        }

        transform.position += (directionVector * Time.fixedDeltaTime);
    }

    public virtual void Hit(RaycastHit hit) {
        IDamageable damageable = hit.collider.GetComponentInParent<IDamageable> ();
        if (damageable != null)
            new Damage (GetDamage (), armorPenetration, weapon).DoDamage (damageable);

        Rigidbody body = hit.rigidbody;
        if (body) {
            body.AddForceAtPosition (transform.forward * GetDamage (), hit.point, ForceMode.Impulse);
        }

        Destroy (gameObject);
        Destroy (Instantiate (hitParticle, hit.point, Quaternion.LookRotation (hit.normal, Vector3.up)), 5f);
    }
}
