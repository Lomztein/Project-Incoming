using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {

    public float damage;
    public float armorPenetration;

    public Damage(float _damage, float _armorPenetration) {
        damage = _damage;
        armorPenetration = _armorPenetration;
    }

    public void DoDamage(IDamageable toDamage) {
        toDamage.Damage (this);
    }

    public static float CalculateDamagePostArmor (float damage, float armorPenetration, float armorRating) {
        float relative = Mathf.Clamp01 (1 - (armorRating - armorPenetration));
        return relative * damage;
    }

    public float CalculateDamagePostArmor(float armorRating) {
        return CalculateDamagePostArmor (damage, armorPenetration, armorRating);
    }
}
