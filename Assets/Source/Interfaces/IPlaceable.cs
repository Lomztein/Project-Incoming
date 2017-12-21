using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable {

    bool ToPosition(Vector3 position, Quaternion rotation);

    bool ToTransform(Transform toTransform);

    bool Place();

    bool PickUp();

}
