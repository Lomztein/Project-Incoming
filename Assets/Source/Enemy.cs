﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable {

    public float health;
    public float armorRating;

    public new Rigidbody rigidbody;
    public GameObject explosion;
    public GameObject debris;

    public float range = 10f;

    public void Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armorRating);
        if (health < 0f)
            Explode ();
    }

    public virtual void Explode() {
        GameObject debrisObject = Instantiate (debris, transform.position, transform.rotation);
        Rigidbody debrisRigid = debrisObject.GetComponent<Rigidbody> ();
        debrisRigid.AddForce (Vector3.up * Random.Range (10f, 15f), ForceMode.VelocityChange);
        debrisRigid.AddTorque (Random.insideUnitSphere * Random.Range (100, 300));

        Destroy (debrisObject, 10f);
        Destroy (Instantiate (explosion, transform.position, transform.rotation), 10f);
        Destroy (gameObject);
    }

}
