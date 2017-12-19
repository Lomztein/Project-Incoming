using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmplacementButton : PurchaseButton {

    public GameObject menuPrefab;
    public Emplacement emplacement;
    public EmplacementMenuGUI menu;

    public override void UpdateInteractable() {
        thisButton.interactable = emplacement.turret || PlayerInput.HasCredits (cost);
    }

    public void OpenEmplacementMenu(Emplacement emplacement) {
        menu.Open ();
    }

    public void OnEmplacementMenuClosed () {
        obj = emplacement.turretPrefab;
        image.texture = Iconography.GenerateIcon (obj);
    }

    public override void Purchase() {
        if (emplacement.turret) {
            OpenEmplacementMenu (emplacement);
        } else {
            EmplacementTurret turret = emplacement.turretPrefab.GetComponent<EmplacementTurret> ();
            if (PlayerInput.TryUseCredits (turret.cost)) {
                emplacement.BuildTurret ();
                menu.Open ();
            }
        }
    }

    public void Initialize() {
        menu = GUIWindowBase.Create (menuPrefab, transform.parent) as EmplacementMenuGUI;

        menu.parentButton = this;
        menu.emplacement = emplacement;
        menu.transform.position = transform.position;
        menu.Init ();

        menu.Close ();
    }
}
