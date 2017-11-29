using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject projectile;
    private IFireable projectileFireable;
    public GameObject fireParticle;
    public AudioClip fireSound;
    public AudioSource audioSource;

    public Transform muzzle;

    private bool isChambered;
    public float rechamberSpeed;
    public float reloadSpeed;

    public int ammo = -1;
    public int maxAmmo = -1;

    private void Awake() {
        projectileFireable = projectile.GetComponent<IFireable> ();
        Invoke ("Rechamber", rechamberSpeed);
    }

    public bool Fire() {
        if (HasAmmo ()) {
            if (isChambered) {
                UseAmmo ();
                projectileFireable.Fire (muzzle, this);

                audioSource.PlayOneShot (fireSound);
                GameObject particle = Instantiate (fireParticle, muzzle.position, muzzle.rotation);
                Destroy (particle, 3f);

                return true;
            }
        }
        return false;
    }

    private bool HasAmmo() {
        return maxAmmo == -1 || ammo > 0;
    }

    private void UseAmmo() {
        isChambered = false;
        Invoke ("Rechamber", rechamberSpeed);

        if (maxAmmo != -1)
            ammo--;

        if (ammo == 0)
            Invoke ("Reload", reloadSpeed);
    }

    private void Rechamber() {
        isChambered = true;
    }

    private void Reload() {
        ammo = maxAmmo;
    }
}
