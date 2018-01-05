using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public GameObject controlledObject;
    private IControllable _controllable;
    public new Camera camera;
    public float sensitivity;

    private float forwardDir;
    private float sideDir;

    private float xRot;
    private float yRot;

    private void Start() {
        _controllable = controlledObject.GetComponent<IControllable> ();
    }

    private void Update() {
        Ray ray = camera.ScreenPointToRay (new Vector3 (Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit)) {
            _controllable.Aim (hit.point);
        } else {
            _controllable.Aim (ray.GetPoint (100f));
        }

        forwardDir = Input.GetAxis ("Vertical");
        sideDir = Input.GetAxis ("Horizontal");

        transform.position = Vector3.Lerp (transform.position, controlledObject.transform.position, 20f * Time.deltaTime);

        xRot += Input.GetAxis ("Mouse X");
        yRot += Input.GetAxis ("Mouse Y");

        transform.rotation = Quaternion.Euler (new Vector3 (yRot * -1f, xRot, 0f) * sensitivity);
    }

    private void FixedUpdate() {
        _controllable.Move (Mathf.RoundToInt (forwardDir));
        _controllable.Turn (sideDir * 45f);

        if (Input.GetButton ("Fire1")) {
            _controllable.Fire ();
        }
    }
}
