using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour {

    public float health;
    public float maxHealth;

    public void Damage(Damage damage) {
        health -= damage.damage;
    }

    void OnTriggerEnter(Collider other) {
        Enemy enemy = other.GetComponentInParent<Enemy> ();
        if (enemy) {
            Damage (new Damage (enemy.value, 0f, null));
            Destroy (enemy.gameObject);
        }
    }
}
