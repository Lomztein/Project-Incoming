using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseMenu : MonoBehaviour {

    public static PurchaseMenu menu;

    public IPlaceable placeableInHand;
    public GameObject hoveringObject;

    public Text defensiveTurretCapacityText;

    public GameObject emplacementButtonPrefab;
    public LayerMask playerLayer;
    
    // Use this for initialization
    private void Awake() {
        menu = this;
    }

    private void Start() {
        InitializeEmplacementMenus ();

        EnemyHandler.OnWaveEnded += () => {
            SetActive (true);
        };

        EnemyHandler.OnWaveStarted += () => {
            SetActive (false);
        };
    }

    // Update is called once per frame
    void Update () {
        if (placeableInHand as Object != null) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
                Transform root = hit.transform.root;
                if (!placeableInHand.ToTransform (root)) {
                    placeableInHand.ToPosition (PlayerInput.mouseWorldPosition, Quaternion.identity);
                }
            }

            if (Input.GetButtonDown ("Fire1")) {
                Place ();
            }
        }

        defensiveTurretCapacityText.text = "Turrets: " + DefensiveTurret.allDefensiveTurrets.Count + " / " + DefensiveTurret.defensiveTurretCapacity;
	}

    public static void PickUp(IPlaceable placeable) {
        if (menu.placeableInHand as Object == null) {
            menu.placeableInHand = placeable;
            placeable.PickUp ();
        }
    }

    public static void Place() {
        if (menu.placeableInHand.Place ()) {
            menu.placeableInHand = null;
        }
    }

    public void SetActive (bool state) {
        gameObject.SetActive (state);
    }

    public void InitializeEmplacementMenus () {
        foreach (Emplacement empl in Emplacement.allEmplacements) {
            GameObject newButton = Instantiate (emplacementButtonPrefab, transform, false);
            newButton.transform.position = Camera.main.WorldToScreenPoint (empl.transform.position);
            EmplacementButton butt = newButton.GetComponent<EmplacementButton> ();

            butt.emplacement = empl;
            butt.Initialize ();
        }
    }
}
