using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheeledVehicle : Vehicle {

    public float motorTorque;

    public Axel frontWheels;
    public Axel backWheels;

    public override void Move (float direction) {
        frontWheels.SetTorque (motorTorque * direction);
    }

    public override void Turn (float angle) {
        frontWheels.SteerTowards (angle);
        frontWheels.SetBrakeTorque (0f);
    }

    public override void Brake () {
        frontWheels.SetTorque (0f);
        frontWheels.SetBrakeTorque (motorTorque);
    }

    private void FixedUpdate () {
        frontWheels.Update ();
        backWheels.Update ();
    }

    [System.Serializable]
    public class Axel {

        public WheelCollider rightSide; 
        public WheelCollider leftSide;
        public float maxAngle = 30;

        public Transform rightSideModel;
        public Transform leftSideModel;

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

        public void Update () {
            Vector3 pos; Quaternion rot;
            rightSide.GetWorldPose (out pos, out rot);
            rightSideModel.position = pos;
            rightSideModel.rotation = rot;

            leftSide.GetWorldPose (out pos, out rot);
            leftSideModel.position = pos;
            leftSideModel.rotation = rot;
        }
    }
}
