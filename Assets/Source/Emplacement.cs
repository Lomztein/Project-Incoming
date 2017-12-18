using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;

public class Emplacement : MonoBehaviour, ILinkable {

    public GameObject turretPrefab;
    public Turret turret;

    public static List<Emplacement> allEmplacements = new List<Emplacement> ();

    public LinkedFire Link {
        get {
            return turret.Link;
        }

        set {
            turret.Link = value;
        }
    }

    public IWeapon Weapon {
        get { return turret.Weapon; }
        set { turret.Weapon = value; }
    } 

    private void Awake () {
        allEmplacements.Add (this);
    }

    private void OnLevelWasLoaded(int level) {
        allEmplacements = new List<Emplacement> ();
    }

    public void Fire() {
        turret.Fire ();
    }

    private void FixedUpdate () {
        if (turret != null && !Aimbot.isPresent) {
            turret.Aim (PlayerInput.mouseWorldPosition);
            if (Input.GetButton ("Fire1"))
                Fire ();
        }
    }

    public void PurchaseTurret() {
        if (PlayerInput.TryUseCredits (500)) {
            BuildTurret ();
        }
    }

    public void BuildTurret() {
        GameObject newTurret = Instantiate (turretPrefab, transform.position, transform.rotation);
        turret = newTurret.GetComponent<Turret> ();
    }

    public void ChangeTurret(GameObject newTurret) {
        turretPrefab = newTurret;
        if (turret)
            Destroy (turret.gameObject);

        BuildTurret ();
     }

    public float GetFirerate() {
        return turret.GetFirerate ();
    }

    public void OnFire () {
        turret.OnFire ();
    }
}
