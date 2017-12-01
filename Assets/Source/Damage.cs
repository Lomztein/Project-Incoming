using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {

    public float damage;
    public float armorPenetration;
    public Weapon weapon;

    public Damage(float _damage, float _armorPenetration, Weapon _weapon) {
        damage = _damage;
        armorPenetration = _armorPenetration;
        weapon = _weapon;
    }

    public void DoDamage(IDamageable toDamage) {
        toDamage.Damage (this);
    }

    public float CalculateDamagePostArmor(float armorRating) {
        float relative = Mathf.Clamp01 (1 - (armorRating - armorPenetration));
        return relative * damage;
    }
}
