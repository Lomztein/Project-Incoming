using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAimable {

    Vector3 TargetPosition {
        get;
    }

    void Aim(Vector3 position);

    void SetIdle();

    bool Fire();

}
