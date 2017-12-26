using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringVehicle : Vehicle {

    [System.Serializable]
    public class Engine {

        public void Init (Rigidbody _rigidbody) {
            rigidbody = _rigidbody;
        }

        public Transform transform;
        private Rigidbody rigidbody;
        public float forceMultiplier;

    }

    public Engine [ ] engines;
    public float targetHeight;
    public float steeringForce;
    public float engineForce;

    public override void Start () {
        base.Start ();
        foreach (Engine engine in engines) {
            engine.Init (GetComponent<Rigidbody>());
        }
    }

    public void FixedUpdate() {
        Fly ();
    }

    public override void Move(float direction) {
        HoverTowards (transform.position + transform.forward * direction, 1f);
    }

    private void HoverTowards (Vector3 position, float forceMultiplier) {
        foreach (Engine engine in engines) {
            float sign = Mathf.Sign (Vector3.Dot (engine.transform.position - (transform.position + GetComponent<Rigidbody>().centerOfMass), (position - transform.position).normalized));
            engine.forceMultiplier = 1f - steeringForce * sign * forceMultiplier;
        }
    }

    public override void Brake() {
        HoverTowards (transform.position - GetComponent<Rigidbody>().velocity, Mathf.Min (GetComponent<Rigidbody>().velocity.magnitude / 10f, 0.5f));
    }

    public override void Turn(float angle) {
        GetComponent<Rigidbody>().AddTorque (transform.rotation * new Vector3 (0f, angle, 0f));
    }

    private void Fly () {
        foreach (Engine engine in engines) {
            Ray ray = new Ray (engine.transform.position, Vector3.down);
            RaycastHit hit;

            Debug.DrawRay (ray.origin, ray.direction * targetHeight);
            float heightForce = 1f;

            if (Physics.Raycast (ray, out hit)) {
                heightForce = 1 + Mathf.Clamp (targetHeight - hit.distance, -1f, 1f);
            }

            float equillibriumForce = GetComponent<Rigidbody>().mass * (Physics.gravity.y * -1f) / engines.Length;
            float relHeight = 1 + (transform.position.y - engine.transform.position.y);

            Vector3 force = engine.transform.up * equillibriumForce * relHeight * heightForce * engine.forceMultiplier;
            GetComponent<Rigidbody>().AddForceAtPosition (force, engine.transform.position);
        }
    }
}
