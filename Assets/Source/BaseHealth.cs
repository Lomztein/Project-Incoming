using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IHasHealthbar {

    public float health;
    public float maxHealth;

    private void Start() {
        BaseHealthbars.AddHealthbar (new BaseHealthbars.Bar (gameObject, Color.green));
    }

    public void Damage(Damage damage) {
        health -= damage.damage;
    }

    public float GetHealthPercentage() {
        return health / maxHealth;
    }

    void OnTriggerEnter(Collider other) {
        Enemy enemy = other.GetComponentInParent<Enemy> ();
        if (enemy) {
            Damage (new Damage (enemy.value, 0f));
            Destroy (enemy.gameObject);
        }
    }
}
