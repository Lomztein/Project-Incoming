using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour {

    public long cost;
    public GameObject obj;
    public Button thisButton;
    public RawImage image;

    private void Awake() {
        thisButton = GetComponent<Button> ();
        PlayerInput.OnCreditsChanged += PlayerInput_OnCreditsChanged;
    }

    private void OnDestroy() {
        PlayerInput.OnCreditsChanged -= PlayerInput_OnCreditsChanged;
    }

    private void Start() {
        if (obj)
            image.texture = Iconography.GenerateIcon (obj);
    }

    private void PlayerInput_OnCreditsChanged(object sender, System.EventArgs e) {
        UpdateInteractable ();
    }

    public virtual void Purchase() {
        if (PlayerInput.TryUseCredits (cost)) {
            GameObject newObject = Instantiate (obj);
            IPlaceable placeable = newObject.GetComponent<IPlaceable> ();
            PurchaseMenu.PickUp (placeable);
        }
    }

    public virtual void UpdateInteractable () {
        thisButton.interactable = PlayerInput.HasCredits (cost);
    }
}
