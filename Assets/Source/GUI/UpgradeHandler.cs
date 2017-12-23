using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandler : MonoBehaviour {

    public UpgradePurchaseButton turretCapacityUpgradeButton;
    public float turretCostIncreaseCoeffecient;

    public void UpgradeTurretCapacity () {
        if (DefensiveTurret.defensiveTurretCapacity < DefensiveTurret.defensiveTurretMaxCapacity) {
            if (PlayerInput.TryUseCredits (turretCapacityUpgradeButton.cost)) {
                DefensiveTurret.defensiveTurretCapacity++;
                turretCapacityUpgradeButton.cost = (Mathf.RoundToInt (turretCapacityUpgradeButton.cost * turretCostIncreaseCoeffecient));
            }
        }

        if (DefensiveTurret.defensiveTurretCapacity == DefensiveTurret.defensiveTurretMaxCapacity) {
            turretCapacityUpgradeButton.fullyUpgraded = true;
        }

        turretCapacityUpgradeButton.UpdateInteractable ();
    }

}
