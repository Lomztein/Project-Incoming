using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlefieldGUI : MonoBehaviour {

    public Slider healthBar;
    public BaseHealth baseHealth;
    public GameObject lostMessage;
    public Text creditsText;

    void Update () {
        healthBar.value = baseHealth.health / baseHealth.maxHealth;
        if (baseHealth.health <= 0 && !lostMessage.activeSelf) {
            lostMessage.SetActive (true);
            Invoke ("Restart", 5f);
        }
        creditsText.text = "CREDITS: <b>" + PlayerInput.GetCredits () + "</b>";
	}

    void Restart() {
        SceneManager.LoadScene (0);
    }
}
