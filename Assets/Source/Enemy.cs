using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable {

    public float health;
    public float armorRating;

    public GameObject debris;

    public void Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armorRating);
        if (health < 0f)
            Explode ();
    }

    public virtual void Explode() {
        Destroy (Instantiate (debris, transform.position, transform.rotation), 10f);
        Destroy (gameObject);
    }

}
