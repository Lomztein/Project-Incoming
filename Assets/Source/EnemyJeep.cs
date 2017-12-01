using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJeep : Enemy, IAimable {

    public float maxSpeed;
    public float motorTorque;
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
    }

    void Awake() {
        _turret = turret.GetComponent<IAimable> ();
    }

    private void FixedUpdate() {
        Aim (transform.position + Quaternion.Euler (0f, moveDirection, 0f) * Vector3.forward);

        Vector3 between = (TargetPosition - transform.position).normalized;
        between.y = 0;
        float angle = Trigonometry.AngleBetween (new Vector3 (transform.forward.x, 0, transform.forward.z), between);
        frontWheels.SteerTowards (angle);

        if (target) {
            _turret.Aim (target.position);

            if (Vector3.Distance (transform.position, target.position) < range) {
                Fire ();
                frontWheels.SetBrakeTorque (motorTorque * 2000f);
                frontWheels.SetTorque (0f);
            } else {
                target = null;
            }
        } else {
            if (rigidbody.velocity.sqrMagnitude < (Mathf.Pow (maxSpeed, 2))) {
                frontWheels.SetTorque (motorTorque);
                frontWheels.SetBrakeTorque (0f);
            } else {
                frontWheels.SetTorque (0);
            }

            target = targetFinder.FindTarget (new Ray (transform.position, transform.forward * range), width, range, targetLayer);
            Debug.DrawRay (transform.position, transform.forward * range, Color.red);
        }
    }

    public bool Fire() {
        return _turret.Fire ();
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

        public void SetTorque(float torque) {
            rightSide.motorTorque = torque;
            leftSide.motorTorque = torque;
        }

        public void SetBrakeTorque(float brakeTorque) {
            rightSide.brakeTorque = brakeTorque;
            leftSide.brakeTorque = brakeTorque;
        }
    }
}
