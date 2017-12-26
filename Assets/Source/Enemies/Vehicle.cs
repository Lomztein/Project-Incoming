using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour, IDamageable, IComparable<Weapon>, IDescribed, IControllable {

    public float health;
    public float maxHealth;
    public float armorRating;
    private bool isDead = false;

    public new Rigidbody rigidbody;
    public GameObject explosion;
    public GameObject debris;
    private Healthbar healthbar;

    public float width = 1f;

    [TextArea]
    public string description;
    public string descriptiveName;
    public string Name { get { return descriptiveName; } set { descriptiveName = value; } }
    public string Description { get { return description; } set { description = value; } }

    public string CompareWith(Weapon other) {
        Projectile proj = other.projectile.GetComponent<Projectile> ();
        float damage = Damage.CalculateDamagePostArmor (proj.GetDamage (), proj.armorPenetration, armorRating);
        float ttk = health / damage / other.GetFirerate ();
        return "Time to kill: " + ttk;
    }

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
        if (_turret as Object)
            _turret.Aim (position);
    }

    public abstract void Move(float direction);
    public abstract void Turn(float direction);
    public abstract void Brake();

    void IDamageable.Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armorRating);
        if (health <= 0f)
            Explode ();
    }

    public virtual void Start () {
        healthbar = HealthbarManager.CreateHealthbar ();

        if (turret)
            _turret = turret.GetComponent<IAimable> ();
    }

    public virtual void Update() {
        if (healthbar) {
            healthbar.SetHealth (GetHealthPercentage ());
            healthbar.transform.position = Camera.main.WorldToScreenPoint (transform.position + Vector3.right * width / 2f + Vector3.right);
        }
    }

    public virtual void Explode() {
        if (!isDead) {
            isDead = true;

            if (debris) {
                GameObject debrisObject = Instantiate (debris, transform.position, transform.rotation);
                Rigidbody [ ] bodies = debrisObject.GetComponentsInChildren<Rigidbody> ();
                foreach (Rigidbody debrisRigid in bodies) {
                    debrisRigid.AddForce (Vector3.up * Random.Range (10f, 15f), ForceMode.VelocityChange);
                    debrisRigid.AddTorque (Random.insideUnitSphere * Random.Range (100, 300), ForceMode.VelocityChange);
                }

                Destroy (debrisObject, 10f);
            }

            Destroy (Instantiate (explosion, transform.position, transform.rotation), 10f);
            Destroy (gameObject);

            Destroy (healthbar.gameObject);
        }
    }

    public float GetHealthPercentage() {
        return health / maxHealth;
    }

    public void SetIdle() {
        if (_turret as Object)
            _turret.SetIdle ();
    }

    public bool Fire() {
        if (_turret as Object)
            return _turret.Fire ();
        return false;
    }
}
