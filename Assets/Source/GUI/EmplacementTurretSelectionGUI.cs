using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmplacementTurretSelectionGUI : MonoBehaviour {

    public GameObject buttonPrefab;

    public Emplacement emplacement;
    public EmplacementPurchaseButton currentTurretButton;
    public List<EmplacementPurchaseButton> allTurretButtons = new List<EmplacementPurchaseButton> ();

    public GameObject [ ] purchaseables;

    public void Init () {
        foreach (GameObject obj in purchaseables) {
            GameObject newButton = Instantiate (buttonPrefab, transform);
            EmplacementPurchaseButton butt = newButton.GetComponent<EmplacementPurchaseButton> ();

            butt.parentGUI = this;
            butt.obj = obj;
            butt.cost = obj.GetComponent<EmplacementTurret> ().cost;

            if (butt.obj == emplacement.turretPrefab) {
                currentTurretButton = butt;
                butt.purchased = true;
            }

            butt.GetComponent<Button> ().onClick.AddListener (() => {
                butt.Purchase ();
            });

            allTurretButtons.Add (butt);
            butt.UpdateInteractable ();
        }
    }

    public void OnPurchase (EmplacementPurchaseButton fromButton) {
        emplacement.ChangeTurret (fromButton.obj);
        currentTurretButton = fromButton;
        UpdateAll ();
    }

    private void UpdateAll () {
        foreach (EmplacementPurchaseButton butt in allTurretButtons) {
            butt.UpdateInteractable ();
        }
    }
}
