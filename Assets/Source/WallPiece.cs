using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPiece : MonoBehaviour, IDamageable, IPlaceable {

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

    public bool PickUp() {
        return true;
    }

    public bool Place() {
        return true;
    }

    public bool ToPosition(Vector3 position, Quaternion rotation) {
        transform.position = position;
        transform.rotation = rotation;
        return true;
    }

    public bool ToTransform(Transform toTransform) {
        return false;
    }
}