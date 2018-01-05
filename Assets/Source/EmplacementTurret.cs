using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmplacementTurret : Turret, IPurchaseable {

    public int cost;
    public string turretName;
    [TextArea]
    public string desc;

    public GameObject [ ] possibleProjectiles;

    public long Cost {
        get { return cost; }
        set { Cost = value; }
    }

    public string Name { get { return turretName; } set { turretName = value; } }
    public string Description { get { return desc; } set { desc = value; } }

}
