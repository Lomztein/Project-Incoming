using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTurret : Turret {

    public float projectileSpeed;

    public override void RotateTurret(Vector3 towards) {
        if (!isIdle) {
            Vector3 transformedPos = yawTransform.InverseTransformPoint (TargetPosition);
            Quaternion rot = Quaternion.LookRotation (transformedPos);
            yawTransform.localRotation = Quaternion.RotateTowards (yawTransform.localRotation, Quaternion.Euler (yawTransform.localRotation.eulerAngles.x, yawTransform.localRotation.eulerAngles.y + rot.eulerAngles.y, yawTransform.localRotation.eulerAngles.z), rotateSpeed * Time.fixedDeltaTime);

            Vector3 relToMuzzle = Weapon.Muzzle.InverseTransformPoint (towards);
            transformedPos -= (Weapon.Muzzle.position - yawTransform.position);
            float angle = Trigonometry.TrajectoryAngle (transformedPos.magnitude, transformedPos.y, projectileSpeed, -Physics.gravity.y);

            if (!float.IsNaN (angle)) {
                rot = Quaternion.Euler (-angle, 0f, 0f);
                pitchTransform.localRotation = Quaternion.RotateTowards (pitchTransform.localRotation, Quaternion.Euler (rot.eulerAngles.x, pitchTransform.localRotation.eulerAngles.y, pitchTransform.localRotation.eulerAngles.z), rotateSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
