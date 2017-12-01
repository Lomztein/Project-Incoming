using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Emplacement : MonoBehaviour {

    public GameObject turretPrefab;
    public Turret turret;

    public static List<Emplacement> allEmplacements = new List<Emplacement> ();

    public Button purchaseButton;
    public LinkedFire link;

    public static LinkedFire mainLink = new LinkedFire ();

    private void Awake() {
        allEmplacements.Add (this);
    }

    private void Start() {
        purchaseButton.transform.position = Camera.main.WorldToScreenPoint (transform.position);
    }

    public void Fire() {
        if (link == null) {
            turret.Fire ();
        } else {
            link.Fire ();
        }
    }

    private void Update() {
        if (turret != null) {
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
        Destroy (purchaseButton.gameObject);
        Link (mainLink);
    }

    public void Link(LinkedFire newLink) {
        newLink.AddEmplacement (this);
    }

    public class LinkedFire {

        public List<Emplacement> emplacements = new List<Emplacement> ();
        public float linkedFireRate = 0;

        private int linkIndex = 0;
        private float readyTime = 0;

        public void AddEmplacement(Emplacement emplacement) {
            bool isSame = emplacements.Count == 0 || emplacements.TrueForAll (x => x.turretPrefab == emplacement.turretPrefab);
            if (isSame) {
                emplacement.link = this;
                emplacements.Add (emplacement);
                linkedFireRate = emplacement.turret.weapon.rechamberSpeed / emplacements.Count;
            }
        }

        public void Fire() {
            if (Time.time > readyTime) {
                if (emplacements [ linkIndex ].turret.Fire ()) {
                    readyTime = Time.time + linkedFireRate;

                    linkIndex++;
                    linkIndex = linkIndex % emplacements.Count;
                }
            }
        }
    }
}
