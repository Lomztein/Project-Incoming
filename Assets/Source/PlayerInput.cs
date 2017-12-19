using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static PlayerInput input;

    public static Vector3 mouseWorldPosition;
    public Emplacement[] emplacements;

    public long credits = 500;

    public static event EventHandler OnCreditsChanged;

    private Vector3 cameraStartingPosition;
    private Quaternion cameraStartingRotation;

    private void Awake() {
        input = this;

        emplacements[0].BuildTurret ();

        cameraStartingPosition = Camera.main.transform.position;
        cameraStartingRotation = Camera.main.transform.rotation;
    }

    public static bool HasCredits(long amount) {
        return input.credits >= amount;
    }

    public static bool TryUseCredits(long amount) {
        if (HasCredits (amount)) {
            GiveCredits (-amount);
            return true;
        }
        return false;
    }

    public static long GetCredits() {
        return input.credits;
    }

    public static void GiveCredits(long amount) {
        input.credits += amount;
        
        if (OnCreditsChanged != null) {
            OnCreditsChanged (null, null);
        }
    }

    private ISupportsFirstPerson cameraFirstPerson;

    void Update() {

        if ((cameraFirstPerson as UnityEngine.Object) == null) {
            Camera.main.transform.position = cameraStartingPosition;
            Camera.main.transform.rotation = cameraStartingRotation;
        } else {
            Camera.main.transform.position = cameraFirstPerson.FirstPersonTransform.position;
            Camera.main.transform.rotation = cameraFirstPerson.FirstPersonTransform.rotation;
        }

        Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Map.gameMap.worldPlaneCollider.Raycast (mouseRay, out hit, Mathf.Infinity)) {
            mouseWorldPosition = hit.point;
        }

        if (Input.GetButtonDown ("Fire2")) {
            if (cameraFirstPerson == null) {
                if (Physics.Raycast (mouseRay, out hit, Mathf.Infinity)) {
                    cameraFirstPerson = hit.transform.root.GetComponentInChildren<ISupportsFirstPerson>();
                }
            } else {
                Camera.main.transform.SetParent (null);
                cameraFirstPerson = null;
            }
        }
    }
}
