using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable : IAimable {

    void Move(float direction);

    void Turn(float direction);

    void Brake();

}
