using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float maxSpeed;
    public float moveDirection;

    public int value;
    public float range = 10f;
    public Vehicle vehicle;

    public Transform target;
    public TargetFinder targetFinder = new TargetFinder ();
    public LayerMask targetLayer;

    private IControllable controllable;

    public Vector3 TargetPosition {
        get {
            return _targetPosition;
        }
    }

    private Vector3 _targetPosition;

    public void Aim(Vector3 position) {
        _targetPosition = position;
    }

    public void Start() {
        controllable = GetComponent<IControllable> ();
    }

    public void SetIdle() {
        if (controllable as Object)
            controllable.SetIdle ();
    }

    public virtual void FixedUpdate() {
        Aim (transform.position + Quaternion.Euler (0f, moveDirection, 0f) * Vector3.forward);

        Vector3 between = (TargetPosition - transform.position).normalized;
        between.y = 0;
        float angle = Trigonometry.AngleBetween (new Vector3 (transform.forward.x, 0, transform.forward.z), between);
        controllable.Turn (angle);

        if (target) {
            if (controllable as Object)
                controllable.Aim (target.position);

            if (target) {
                Fire ();
                controllable.Brake ();
            }
        } else {
            if (vehicle.rigidbody.velocity.magnitude < maxSpeed) {
                controllable.Move (1);
            } else {
                controllable.Brake ();
            }

            SetIdle ();
            target = targetFinder.FindTarget (new Ray (transform.position, transform.forward * range), vehicle.width, range, targetLayer);
            Debug.DrawRay (transform.position, transform.forward * range, Color.red);
        }
    }

    public bool Fire() {
        if (controllable as Object)
            return controllable.Fire ();
        else
            return false;
    }
}