using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlefieldGUI : MonoBehaviour {

    public Slider healthBar;
    public DamageableTurret playerTurret;
    public GameObject lostMessage;
	
	// Update is called once per frame
	void Update () {
        healthBar.value = playerTurret.health / playerTurret.maxHealth;
        if (playerTurret.health < 0 && !lostMessage.activeSelf) {
            lostMessage.SetActive (true);
            Invoke ("Restart", 5f);
        }
	}

    void Restart() {
        SceneManager.LoadScene (0);
    }
}
