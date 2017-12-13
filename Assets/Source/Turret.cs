using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IAimable, ILinkable {

    public Transform yawTransform;
    public Transform pitchTransform;

    public float rotateSpeed;

    public Weapon weapon;
    public Animator animator;

    public bool isIdle = true;
    public bool ignoreDirection;

    public Vector3 TargetPosition {
        get { return _targetPosition; }
    }

    private LinkedFire _linkedFire;
    public LinkedFire Link {
        get { return _linkedFire;  }
        set { _linkedFire = value; }
    }

    public IWeapon Weapon {
        get { return weapon; }
    }

    private Vector3 _targetPosition;

    public virtual void FixedUpdate() {
        // Theres some heavy mathematics going on here, stuff I *technically* know, but am not particularily good at.
        if (!isIdle) {
            Vector3 transformedPos = yawTransform.InverseTransformPoint (TargetPosition);

            Quaternion rot = Quaternion.LookRotation (transformedPos);

            yawTransform.localRotation = Quaternion.RotateTowards (yawTransform.localRotation, Quaternion.Euler (yawTransform.localRotation.eulerAngles.x, yawTransform.localRotation.eulerAngles.y + rot.eulerAngles.y, yawTransform.localRotation.eulerAngles.z), rotateSpeed * Time.fixedDeltaTime);

            if (pitchTransform) {
                transformedPos = pitchTransform.InverseTransformPoint (TargetPosition);
                rot = Quaternion.LookRotation (transformedPos, Vector3.right);

                pitchTransform.localRotation = Quaternion.RotateTowards (pitchTransform.localRotation, Quaternion.Euler (pitchTransform.localRotation.eulerAngles.x + rot.eulerAngles.x, pitchTransform.localRotation.eulerAngles.y, pitchTransform.localRotation.eulerAngles.z), rotateSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void Aim(Vector3 position) {
        _targetPosition = position;
        isIdle = false;
    }

    public void SetIdle() {
        isIdle = true;
    }

    public bool Fire() {
        if (_linkedFire != null) {
            return _linkedFire.Fire ();
        }

        float deltaAngle = Vector3.Angle (TargetPosition - weapon.muzzle.position, weapon.muzzle.forward);

        if (deltaAngle < 2f || ignoreDirection) {
            if (weapon.Fire ()) {
                OnFire ();
                return true;
            }
        }
        return false;
    }

    private void PlayFireAnimation () {
        if (animator) {
            animator.SetBool ("Firing", true);
            Invoke ("StopFire", 0.1f);
        }
    }
        
    private void StopFire() {
        animator.SetBool ("Firing", false);
    }

    public float GetFirerate() {
        return Weapon.GetFirerate ();
    }

    public void OnFire() {
        PlayFireAnimation ();
    }
}
