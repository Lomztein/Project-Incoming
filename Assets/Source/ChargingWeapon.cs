using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingWeapon : Weapon {

    public float charge;
    public float chargeTreshold;

    public AudioSource chargeSource;
    public AnimationCurve chargePitch;

    public override bool CanFire () {
        charge += Time.fixedDeltaTime * 2f;

        return base.CanFire () && charge >= chargeTreshold;
    }

    public virtual void FixedUpdate () {
        charge -= Time.fixedDeltaTime;
        if (charge > 0.1f) {
            if (!chargeSource.isPlaying)
                chargeSource.Play ();
            float evaluation = chargePitch.Evaluate (charge / chargeTreshold);
            chargeSource.pitch = evaluation;
        } else if (chargeSource.isPlaying)
            chargeSource.Stop ();

        charge = Mathf.Clamp (charge, 0f, chargeTreshold);
    }
}
