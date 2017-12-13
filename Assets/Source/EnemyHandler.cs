using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {

    public static EnemyHandler enemyHandler;

    public GameObject enemy;
    public float spawnDelay = 2f;
    public float minSpawnDelay = 0.5f;

    public static bool waveStarted = false;
    public static int waveCount;

    public float spawnXRange = 20;
    public float spawnDelayLowering = 0.5f;
    public int spawnPerWave = 5;
    public int spawnAmount;

    // Use this for initialization
    private void Awake() {
        enemyHandler = this;
    }

    private void Start() {
        EndWave ();
    }

    void Spawn() {
        GameObject newEnemy = Instantiate (enemy, transform.position + (Random.Range (-spawnXRange, spawnXRange) * transform.right), transform.rotation);
        spawnAmount--;

        if (spawnAmount > 0)
            Invoke ("Spawn", spawnDelay);
        else
            EndWave ();
    }

    public void StartWave() {
        waveStarted = true;
        waveCount++;

        enemyHandler.spawnDelay -= enemyHandler.spawnDelayLowering;
        enemyHandler.spawnAmount = enemyHandler.spawnPerWave * waveCount;

        enemyHandler.Spawn ();
    }

    public void EndWave() {
        waveStarted = false;
        Invoke ("StartWave", 15f);
    }

}
