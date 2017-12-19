using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon {

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

    public LayerMask hittableLayer;

    public Transform Muzzle {
        get {
            return muzzle;
        }
    }

    private void Awake() {
        SetProjectile (projectile);
        Invoke ("Rechamber", rechamberSpeed);
    }

    public void SetProjectile (GameObject newProjectile) {
        projectile = newProjectile;
        projectileFireable = projectile.GetComponent<IFireable> ();
    }

    public GameObject GetProjectilePrefab () {
        return projectile;
    }

    public bool Fire() {
        if (CanFire ()) {
            UseAmmo ();
            projectileFireable.Fire (muzzle, this);

            audioSource.PlayOneShot (fireSound);
            GameObject particle = Instantiate (fireParticle, muzzle.position, muzzle.rotation);
            Destroy (particle, 3f);

            return true;
        }
        return false;
    }

    public virtual bool CanFire () {
        return HasAmmo () && isChambered;
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

    public float GetDamage() {
        return projectile.GetComponent<Projectile> ().GetDamage ();
    }

    public float GetDPS() {
        return projectile.GetComponent<Projectile> ().GetDamage () / rechamberSpeed;
    }

    public float GetFirerate() {
        return rechamberSpeed;
    }
}
