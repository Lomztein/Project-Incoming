using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public static PlayerInput input;

    public static Vector3 mouseWorldPosition;
    public Emplacement[] emplacements;

    private TargetFinder aimbot = new TargetFinder ();
    private Transform aimbotTarget;
    public LayerMask aimbotLayer;
    public bool aimbotEnabled;

    public long credits = 500;

    private void Awake() {
        input = this;

        emplacements[0].BuildTurret ();
    }

    public static bool HasCredits(long amount) {
        return input.credits >= amount;
    }

    public static bool TryUseCredits(long amount) {
        if (input.credits >= amount) {
            input.credits -= amount;
            return true;
        }
        return false;
    }

    public static long GetCredits() {
        return input.credits;
    }

    public static void GiveCredits(long amount) {
        input.credits += amount;
    }

	// Update is called once per frame
	void Update () {
        if (aimbotEnabled) {
            if (!aimbotTarget) {
                aimbotTarget = aimbot.FindTarget (Vector3.zero, 50, aimbotLayer);
            } else
                mouseWorldPosition = aimbotTarget.position;
        } else {
            Ray mouseRay = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Map.gameMap.worldPlaneCollider.Raycast (mouseRay, out hit, Mathf.Infinity)) {
                mouseWorldPosition = hit.point;
            }
        }
    }
}
