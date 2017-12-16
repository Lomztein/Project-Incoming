using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IAimable, ILinkable {

    public Transform yawTransform;
    public Transform pitchTransform;

    public float rotateSpeed;
    [Range (0f, 85f)]
    public float elevationRange = 85f;

    public GameObject weapon;
    private IWeapon _weapon;
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
        get { return _weapon; }
        set { _weapon = value; }
    }

    private Vector3 _targetPosition;

    private void Awake() {
        Weapon = weapon.GetComponent<IWeapon> ();
    }

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
                if (pitchTransform.localRotation.eulerAngles.x > 180) { // Looking upwards.
                    if (pitchTransform.localRotation.eulerAngles.x < 360 - elevationRange) {
                        pitchTransform.localRotation = Quaternion.Euler (360f - elevationRange, 0f, 0f);
                    }
                } else { // Looking downwards.
                    if (pitchTransform.localRotation.eulerAngles.x > elevationRange) {
                        pitchTransform.localRotation = Quaternion.Euler (elevationRange, 0f, 0f);
                    }
                }
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

        float deltaAngle = Vector3.Angle (TargetPosition - Weapon.Muzzle.position, Weapon.Muzzle.forward);

        if (deltaAngle < 2f || ignoreDirection) {
            if (Weapon.Fire ()) {
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
