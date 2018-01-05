using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    public Dictionary<string, bool> purchasedTurrets = new Dictionary<string, bool> ();
    public Dictionary<string, bool> purchasedAmmo = new Dictionary<string, bool> ();

    public override void Open() {
        gameObject.SetActive (true);
        parentButton.gameObject.SetActive (false);
    }

    public override void Close() {
        gameObject.SetActive (false);
        parentButton.gameObject.SetActive (true);
        parentButton.OnEmplacementMenuClosed ();
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
            butt.turretPrefab = obj.GetComponent<EmplacementTurret> ();

            FeedButtonData (butt, obj);
            butt.cost = obj.GetComponent<EmplacementTurret> ().cost;

            if (butt.obj == emplacement.turretPrefab) {
                currentTurretButton = butt;
                SetTurretPurchased (butt.turretPrefab.Name);
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
                SetProjectilePurchased (butt.obj.name);
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
        SetTurretPurchased (fromButton.turretPrefab.Name);
        emplacement.ChangeTurret (fromButton.obj);
        currentTurretButton = fromButton;
        RebuildButtons ();
    }

    public void OnSelectProjectile (EmplacementProjectileSelectButton fromButton) {
        SetProjectilePurchased (fromButton.obj.name);
        emplacement.ChangeProjectile (fromButton.obj);
        currentProjectileButton = fromButton;
        UpdateAll ();
    }

    public void SetProjectilePurchased (string name) {
        if (!purchasedAmmo.ContainsKey (name))
            purchasedAmmo.Add (name, true);
    }

    public void SetTurretPurchased (string name) {
        if (!purchasedTurrets.ContainsKey (name))
            purchasedTurrets.Add (name, true);
    }

    public bool IsTurretPurchased (string turretName) {
        return IsPurchased (purchasedTurrets, turretName);
    }

    public bool IsProjectilePurchased(string projName) {
        return IsPurchased (purchasedAmmo, projName);
    }

    private bool IsPurchased(Dictionary<string, bool> dict, string entry) {
        if (!dict.ContainsKey (entry))
            return false;
        return dict[ entry];
    }

    private void UpdateAll() {
        foreach (EmplacementMenuPurchaseButtonBase butt in allButtons) {
            butt.UpdateInteractable ();
        }
    }

}
