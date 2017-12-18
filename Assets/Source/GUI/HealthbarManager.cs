using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarManager : MonoBehaviour {

    public static HealthbarManager healthbarManager;

    public GameObject healthbarPrefab;

    private void Awake() {
        healthbarManager = this;
    }

    public static Healthbar CreateHealthbar () {
        return Instantiate (healthbarManager.healthbarPrefab, Vector3.right * 9001, Quaternion.identity, healthbarManager.transform).GetComponent<Healthbar>();
    }

}
