using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IAimable {

    public Transform yawTransform;
    public Transform pitchTransform;

    public float rotateSpeed;

    public Weapon weapon;
    public Animator animator;

    public Vector3 TargetPosition {
        get {
            return _targetPosition;
        }
    }

    private Vector3 _targetPosition;

	// Use this for initialization
	void Start () {
        PlayerInput.AddControlledAimable (this);
	}

    private void FixedUpdate() {
        float angle = Trigonometry.CalculateAngleXZ (transform.position, TargetPosition);
        yawTransform.rotation = Quaternion.RotateTowards (yawTransform.rotation, Quaternion.Euler (0f, angle, 0f), rotateSpeed * Time.fixedDeltaTime);
    }

    public void Aim(Vector3 position) {
        _targetPosition = position;
    }

    public void Fire() {
        if (weapon.Fire ()) {
            animator.SetBool ("Firing", true);
            Invoke ("StopFire", 0.1f);
        }
    }

    private void StopFire() {
        animator.SetBool ("Firing", false);
    }
}
