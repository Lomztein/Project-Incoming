using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Trigonometry {

    public static float CalculateAngleXY(Vector3 from, Vector3 to) {
        return -Mathf.Rad2Deg * Mathf.Atan2 (to.y - from.y, to.x - from.x) - 180;
    }

    public static float CalculateAngleXZ(Vector3 from, Vector3 to) {
        return -Mathf.Rad2Deg * Mathf.Atan2 (to.z - from.z, to.x - from.x) - 180;
    }
}
