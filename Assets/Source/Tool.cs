using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool<T> : MonoBehaviour, IPlaceable {

    protected Transform lastTransform;
    protected T item;

    public bool PickUp() {
        return true;
    }

    public abstract bool Place();

    public bool ToPosition(Vector3 position, Quaternion rotation) {
        transform.position = position;

        lastTransform = null;
        item = default (T);

        return true;
    }

    public virtual bool ToTransform(Transform toTransform) {
        if (lastTransform != toTransform) {
            lastTransform = toTransform;
            item = toTransform.root.GetComponentInChildren<T> ();
        }

        if (item as Object) {
            transform.position = toTransform.position;
            return true;
        }

        return false;
    }

    protected void Done () {
        Destroy (gameObject);
    }
}
