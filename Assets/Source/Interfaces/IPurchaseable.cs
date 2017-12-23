using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPurchaseable : IDescribed {

    long Cost {
        get; set;
    }

}
