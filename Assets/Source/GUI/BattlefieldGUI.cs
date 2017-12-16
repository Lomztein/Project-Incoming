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

    public Button startWaveButton;

    private void Start() {
        EnemyHandler.OnWaveEnded += (obj, args) => {
            startWaveButton.interactable = !EnemyHandler.waveStarted;
        };
    }

    void Update () {
        healthBar.value = baseHealth.health / baseHealth.maxHealth;
        if (baseHealth.health <= 0 && !lostMessage.activeSelf) {
            lostMessage.SetActive (true);
            Invoke ("Restart", 5f);
        }
        creditsText.text = "CREDITS: <b>" + PlayerInput.GetCredits () + "</b>";
	}

    public void StartWave () {
        EnemyHandler.StaticStartWave ();
        startWaveButton.interactable = !EnemyHandler.waveStarted;
    }

    void Restart() {
        SceneManager.LoadScene (0);
    }
}
