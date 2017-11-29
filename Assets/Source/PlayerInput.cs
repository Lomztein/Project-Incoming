using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static PlayerInput input;

    public static Vector3 mouseWorldPosition;
    private List<IAimable> controlledAimables = new List<IAimable> ();

    private void Awake() {
        input = this;
    }

    public static void AddControlledAimable(IAimable aimable) {
        input.controlledAimables.Add (aimable);
    }

	// Update is called once per frame
	void Update () {
        Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Map.gameMap.worldPlaneCollider.Raycast (mouseRay, out hit, Mathf.Infinity)) {
            mouseWorldPosition = hit.point;

            foreach (IAimable aimable in controlledAimables) {
                aimable.Aim (mouseWorldPosition);
            }
        }

        if (Input.GetButton ("Fire1")) {
            foreach (IAimable aimable in controlledAimables) {
                aimable.Fire ();
            }
        }
	}
}
