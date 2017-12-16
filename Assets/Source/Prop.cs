using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour, IDamageable {

    public new Rigidbody rigidbody;

    public float health;
    public float armor;

    public void Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armor);

        if (health <= 0f)
            Destroy (gameObject);
    }
}
