using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable, IHasHealthbar {

    public float health;
    public float maxHealth;
    public float regenSpeed;

    public AnimationCurve postDamageReductionCurve;
    public float postDamageReductionTime;
    public float lastDamageTime;

    private void Start() {
        BaseHealthbars.AddHealthbar (new BaseHealthbars.Bar (gameObject, Color.cyan));
        lastDamageTime = Time.time;
    }

    private void FixedUpdate() {
        if (health < maxHealth)
            health += regenSpeed * Time.fixedDeltaTime * Mathf.Clamp01 (Time.time - lastDamageTime);
        else
            health = maxHealth;
    }

    public void Damage(Damage damage) {
        float relTime = Mathf.Clamp01 (Time.time - lastDamageTime / postDamageReductionTime);
        float damageMultiplier = postDamageReductionCurve.Evaluate (relTime);
        health -= damage.damage * damageMultiplier; // Ignore armor penetration completely.
        lastDamageTime = Time.time;
    }

    public float GetHealthPercentage() {
        return health / maxHealth;
    }
}
