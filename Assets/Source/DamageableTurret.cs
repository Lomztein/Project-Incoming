using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTurret : Turret, IDamageable {

    public float health = 1000f;
    public float maxHealth = 1000f;
    public float armor = 1f;

    public void Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armor);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
