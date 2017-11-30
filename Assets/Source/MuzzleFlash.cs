using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

    public float shrinkSpeed = 0.2f;
    public int shrinkIterations = 5;

    public void Start() {
        StartCoroutine (_Shrink ());
    }

    private IEnumerator _Shrink() {
        for (int i = 0; i < shrinkIterations; i++) {
            transform.localScale *= shrinkSpeed;
            yield return new WaitForFixedUpdate ();
        }
        Destroy (gameObject);
    }

}
