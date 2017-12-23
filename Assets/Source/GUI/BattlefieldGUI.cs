using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlefieldGUI : MonoBehaviour {

    public BaseHealth baseHealth;
    public GameObject lostMessage;
    public Text creditsText;

    public RectTransform nextWaveParent;

    public GameObject enemyEntryPrefab;

    public Button startWaveButton;

    private void Start() {
        UpdateNextWaveContent ();

        EnemyHandler.OnWaveEnded += () => {
            startWaveButton.interactable = !EnemyHandler.waveStarted;
            UpdateNextWaveContent ();
        };
    }

    private void UpdateNextWaveContent () {
        int [ ] amounts = EnemyHandler.enemyHandler.CalculateSpawnAmount (EnemyHandler.waveCount + 1, EnemyHandler.GetSpawnAmount (EnemyHandler.waveCount + 1));
        foreach (Transform child in nextWaveParent) {
            Destroy (child.gameObject);
        }

        for (int i = 0; i < amounts.Length; i++) {
            if (amounts [ i ] != 0) {
                GameObject newEntry = Instantiate (enemyEntryPrefab, nextWaveParent);
                WaveEnemyEntry entry = newEntry.GetComponent<WaveEnemyEntry> ();

                entry.enemyObject = EnemyHandler.enemyHandler.enemies [ i ].enemy;
                entry.count = amounts [ i ];
                entry.UpdateGUI ();

                // Reverse order so that easier enemies are at the top.
                newEntry.transform.SetAsFirstSibling ();
            }
        }
    }

    void Update () {
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
