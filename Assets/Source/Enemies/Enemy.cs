using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable, IComparable<Weapon>, IDescribed {

    public float health;
    public float maxHealth;
    public float armorRating;
    private bool isDead = false;

    public new Rigidbody rigidbody;
    public GameObject explosion;
    public GameObject debris;
    private Healthbar healthbar;

    public float moveDirection = 180f;
    public Transform target;
    public TargetFinder targetFinder = new TargetFinder ();
    public LayerMask targetLayer;

    public int value;

    public float range = 10f;
    public float width = 1f;

    [TextArea]
    public string description;
    public string Name { get { return name; } set { name = value; } }
    public string Description { get { return description; } set { description = value; } }

    public string CompareWith(Weapon other) {
        Projectile proj = other.projectile.GetComponent<Projectile> ();
        float damage = Damage.CalculateDamagePostArmor (proj.GetDamage (), proj.armorPenetration, armorRating);
        float ttk = health / damage / other.GetFirerate ();
        return "Time to kill: " + ttk;
    }

    void IDamageable.Damage(Damage damage) {
        health -= damage.CalculateDamagePostArmor (armorRating);
        if (health <= 0f)
            Explode ();
    }

    public virtual void Start () {
        healthbar = HealthbarManager.CreateHealthbar ();
    }

    public virtual void Update() {
        healthbar.SetHealth (GetHealthPercentage ());
        healthbar.transform.position = Camera.main.WorldToScreenPoint (transform.position + Vector3.right * width / 2f + Vector3.right);
    }

    public virtual void Explode() {
        if (!isDead) {
            isDead = true;
            PlayerInput.GiveCredits (value);

            GameObject debrisObject = Instantiate (debris, transform.position, transform.rotation);
            Rigidbody [ ] bodies = debrisObject.GetComponentsInChildren<Rigidbody> ();
            foreach (Rigidbody debrisRigid in bodies) {
                debrisRigid.AddForce (Vector3.up * Random.Range (10f, 15f), ForceMode.VelocityChange);
                debrisRigid.AddTorque (Random.insideUnitSphere * Random.Range (100, 300), ForceMode.VelocityChange);
            }

            Destroy (debrisObject, 10f);

            Destroy (Instantiate (explosion, transform.position, transform.rotation), 10f);
            Destroy (gameObject);

            Destroy (healthbar.gameObject);
        }
    }

    public float GetHealthPercentage() {
        return health / maxHealth;
    }
}
