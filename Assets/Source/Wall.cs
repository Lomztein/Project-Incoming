using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IDamageable {

    public float health;
    public float maxHealth;
    public float armor;

    public void Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armor);

        if (health < 0f) {
            Destroy (gameObject);
        }

        transform.position += Vector3.down * (damage.damage / maxHealth);
    }
}
