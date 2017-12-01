using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJeep : Enemy, IAimable {

    public float motorTorque;
    public Transform target;
    public Rigidbody rigidBody;

    public Axel frontWheels;
    public GameObject turret;
    private IAimable _turret;

    public Vector3 TargetPosition {
        get {
            return _targetPosition;
        }
    }
    private Vector3 _targetPosition;

    public void Aim(Vector3 position) {
        _targetPosition = position;
        _turret.Aim (position);
    }

    void Awake() {
        target = GameObject.FindGameObjectWithTag ("Player").transform; // Todo, replace this you bafoon.
        _turret = turret.GetComponent<IAimable> ();
    }

    private void FixedUpdate() {
        if (target) {
            Aim (target.position);

            Vector3 between = (TargetPosition - transform.position).normalized;
            between.y = 0;
            float angle = Trigonometry.AngleBetween (new Vector3 (transform.forward.x, 0, transform.forward.z), between);

            frontWheels.SteerTowards (angle);

            if (Vector3.Distance (transform.position, target.position) < range) {
                Fire ();
                frontWheels.Accelerate (0f);
            } else {
                frontWheels.Accelerate (motorTorque);
            }
        }
    }

    public void Fire() {
        _turret.Fire ();
    }

    [System.Serializable]
    public class Axel {

        public WheelCollider rightSide;
        public WheelCollider leftSide;
        public float maxAngle = 30;

        public void SteerTowards(float angle) {
            angle = Mathf.Clamp (angle, -maxAngle, maxAngle);
            rightSide.steerAngle = angle;
            leftSide.steerAngle = angle;
        }

        public void Accelerate(float torque) {
            rightSide.motorTorque = torque;
            leftSide.motorTorque = torque;
        }
    }
}
