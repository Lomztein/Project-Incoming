using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingEnemy : Enemy, IAimable {

    public float maxSpeed;

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

    public void SetIdle() {
        _turret.SetIdle ();
    }

    private void FixedUpdate() {
        Aim (transform.position + Quaternion.Euler (0f, moveDirection, 0f) * Vector3.forward);

        Vector3 between = (TargetPosition - transform.position).normalized;
        between.y = 0;
        float angle = Trigonometry.AngleBetween (new Vector3 (transform.forward.x, 0, transform.forward.z), between);
        TurnTowards (angle);

        if (target) {
            _turret.Aim (target.position);

            if (Vector3.Distance (transform.position, target.position) < range) {
                Fire ();
                Brake ();
            } else {
                target = null;
            }
        } else {
            if (rigidbody.velocity.magnitude < maxSpeed) {
                Drive (1);
            } else {
                Drive (0);
            }

            _turret.SetIdle ();
            target = targetFinder.FindTarget (new Ray (transform.position, transform.forward * range), width, range, targetLayer);
            Debug.DrawRay (transform.position, transform.forward * range, Color.red);
        }
    }

    public virtual void Drive(int direction) {

    }

    public virtual void TurnTowards(float angle) {

    }

    public virtual void Brake() {

    }

    public bool Fire() {
        return _turret.Fire ();
    }
}