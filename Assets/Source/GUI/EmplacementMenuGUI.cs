using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmplacementMenuGUI : MonoBehaviour {

    public EmplacementTurretSelectionGUI turretSelectionGUI;
    public Emplacement emplacement;

    public void Init () {
        turretSelectionGUI.emplacement = emplacement;
        turretSelectionGUI.Init ();
    }

}
