using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IComparable<Weapon> {

    public float health;
    public float armorRating;

    public new Rigidbody rigidbody;
    public GameObject explosion;
    public GameObject debris;

    public float moveDirection = 180f;
    public Transform target;
    public TargetFinder targetFinder = new TargetFinder ();
    public LayerMask targetLayer;

    public int value;

    public float range = 10f;
    public float width = 1f;

    void Start () {
        Debug.Log (CompareWith (Emplacement.allEmplacements[0].turret.Weapon as Weapon));
    }

    public string CompareWith(Weapon other) {
        Projectile proj = other.projectile.GetComponent<Projectile> ();
        float damage = Damage.CalculateDamagePostArmor (proj.GetDamage (), proj.armorPenetration, armorRating);
        float ttk = health / damage / other.GetFirerate ();
        return "Time to kill: " + ttk;
    }

    void IDamageable.Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armorRating);
        if (health <= 0f)
            Explode ();
    }

    public virtual void Explode() {
        PlayerInput.GiveCredits (value);

        GameObject debrisObject = Instantiate (debris, transform.position, transform.rotation);
        Rigidbody debrisRigid = debrisObject.GetComponent<Rigidbody> ();
        debrisRigid.AddForce (Vector3.up * Random.Range (10f, 15f), ForceMode.VelocityChange);
        debrisRigid.AddTorque (Random.insideUnitSphere * Random.Range (100, 300), ForceMode.VelocityChange);

        Destroy (debrisObject, 10f);
        Destroy (Instantiate (explosion, transform.position, transform.rotation), 10f);
        Destroy (gameObject);
    }

}
