using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedWeapons : IWeapon {

    public Weapon [ ] weapons;

    public float sequenceFireTime;
    public float nextFireTime;

    private int weaponIndex;

    public Transform Muzzle {
        get {
            return weapons [ weaponIndex ].muzzle;
        }
    }

    public bool Fire() {
        bool fired = false;
        for (int i = 0; i < weapons.Length; i++) {
            fired = weapons [ i ].Fire ();
        }
        return fired;
    }

    public float GetDamage() {
        return weapons [ 0 ].GetDamage () * weapons.Length;
    }

    public float GetDPS() {
        return weapons [ 0 ].GetDPS () * weapons.Length;
    }

    public float GetFirerate() {
        return weapons [ 0 ].GetFirerate () * weapons.Length;
    }

    public void Reload() {
        foreach (Weapon weapon in weapons)
            weapon.Reload ();
    }
}
