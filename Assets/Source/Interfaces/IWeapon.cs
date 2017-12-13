using UnityEngine;

public interface IWeapon {

    Transform Muzzle {
        get;
    }

    bool Fire();

    float GetFirerate();

    float GetDamage();

    float GetDPS();

}
