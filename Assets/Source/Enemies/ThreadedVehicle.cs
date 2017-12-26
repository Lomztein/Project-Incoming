using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadedVehicle : Vehicle {

    public Thread rightSide;
    public Thread leftSide;

    public float motorTorque;

    public override void Move(float direction) {
        rightSide.SetTorque (motorTorque * direction);
        leftSide.SetTorque (motorTorque * direction);
    }

    public override void Turn(float angle) {
        if (angle > 1f) {
            rightSide.SetTorque (-motorTorque);
            leftSide.SetTorque (motorTorque);
        } else if (angle < -1f) {
            rightSide.SetTorque (motorTorque);
            leftSide.SetTorque (-motorTorque);
        }
    }

    public override void Brake() {
        rightSide.Brake (motorTorque);
        leftSide.Brake (motorTorque);
    }

    [System.Serializable]
    public class Thread {

        public WheelCollider [ ] wheelColliders;
        public Material material;

        public void SetTorque (float torque) {
            for (int i = 0; i < wheelColliders.Length; i++) {
                wheelColliders [ i ].motorTorque = torque;
                wheelColliders [ i ].brakeTorque = 0f;
            }
        }

        public void Brake (float torque) {
            for (int i = 0; i < wheelColliders.Length; i++) {
                wheelColliders [ i ].brakeTorque = torque;
                wheelColliders [ i ].motorTorque = 0f;
            }
        }
    }
}
