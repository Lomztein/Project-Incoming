using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceable {

    void ToPosition(Vector3 position, Quaternion rotation);

    void ToTransform(Transform transform);

    void Place();

}
