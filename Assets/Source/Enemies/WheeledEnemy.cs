using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheeledEnemy : AttackingEnemy {

    public float motorTorque;

    public Axel frontWheels;

    public override void Drive (int direction) {
        frontWheels.SetTorque (motorTorque * direction);
    }

    public override void TurnTowards (float angle) {
        frontWheels.SteerTowards (angle);
    }

    public override void Brake () {
        frontWheels.SetTorque (0f);
        frontWheels.SetBrakeTorque (motorTorque);
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
