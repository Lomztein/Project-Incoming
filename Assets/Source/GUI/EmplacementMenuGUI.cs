using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmplacementMenuGUI : GUIWindowBase {

    public EmplacementButton parentButton;
    public Emplacement emplacement;

    public GameObject turretButtonPrefab;
    public GameObject projectileButtonPrefab;

    public Transform turretButtonParent;
    public Transform projectileButtonParent;

    public EmplacementMenuPurchaseButtonBase currentTurretButton;
    public EmplacementMenuPurchaseButtonBase currentProjectileButton;
    
    public List<EmplacementMenuPurchaseButtonBase> allButtons = new List<EmplacementMenuPurchaseButtonBase> ();

    public GameObject [ ] purchaseableTurrets;
    public GameObject [ ] projectileOptions;

    public override void Open() {
        gameObject.SetActive (true);
        parentButton.gameObject.SetActive (false);
    }

    public override void Close() {
        gameObject.SetActive (false);
        parentButton.gameObject.SetActive (true);
    }

    public void Init() {
        RebuildButtons ();
    }

    private void RebuildButtons () {
        foreach (EmplacementMenuPurchaseButtonBase bBase in allButtons) {
            Destroy (bBase.gameObject);
        }
        allButtons.Clear ();

        CreateTurretPurchaseButtons ();
        RefreshProjectileOptions ();
        UpdateAll ();
    }

    private void CreateTurretPurchaseButtons () {
        foreach (GameObject obj in purchaseableTurrets) {
            GameObject newButton = Instantiate (turretButtonPrefab, turretButtonParent);
            EmplacementTurretPurchaseButton butt = newButton.GetComponent<EmplacementTurretPurchaseButton> ();

            FeedButtonData (butt, obj);
            butt.cost = obj.GetComponent<EmplacementTurret> ().cost;

            if (butt.obj == emplacement.turretPrefab) {
                currentTurretButton = butt;
                butt.purchased = true;
            }
        }
    }

    private void RefreshProjectileOptions () {
        projectileOptions = emplacement.GetProjectileOptions ();

        foreach (GameObject obj in projectileOptions) {
            GameObject newButton = Instantiate (projectileButtonPrefab, projectileButtonParent);
            EmplacementProjectileSelectButton butt = newButton.GetComponent<EmplacementProjectileSelectButton> ();
            FeedButtonData (butt, obj);

            if (butt.obj == emplacement.GetProjectilePrefab ()) {
                currentProjectileButton = butt;
            }
        }
    }

    private void FeedButtonData (EmplacementMenuPurchaseButtonBase butt, GameObject obj) {
        butt.parentGUI = this;
        butt.obj = obj;

        butt.GetComponent<Button> ().onClick.AddListener (() => {
            butt.Purchase ();
        });

        allButtons.Add (butt);
    }

    public void OnSelectTurret (EmplacementTurretPurchaseButton fromButton) {
        emplacement.ChangeTurret (fromButton.obj);
        currentTurretButton = fromButton;
        RebuildButtons ();
    }

    public void OnSelectProjectile (EmplacementProjectileSelectButton fromButton) {
        emplacement.ChangeProjectile (fromButton.obj);
        currentProjectileButton = fromButton;
        UpdateAll ();
    }

    private void UpdateAll() {
        foreach (EmplacementMenuPurchaseButtonBase butt in allButtons) {
            butt.UpdateInteractable ();
        }
    }

}
