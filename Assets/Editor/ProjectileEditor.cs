using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Projectile))]
public class ProjectileEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI ();

        Projectile projectile = (Projectile)target;
        EditorGUILayout.LabelField ("Damage", projectile.GetDamage ().ToString ());
    }
}
