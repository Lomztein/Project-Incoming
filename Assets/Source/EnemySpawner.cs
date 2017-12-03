using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public float spawnDelay = 2f;
    public float minSpawnDelay = 0.5f;

    public float spawnXRange = 20;
    public float spawnDelayLoweringTime = 30f;

	// Use this for initialization
	void Start () {
        Invoke ("Spawn", 2f);
	}

    void Spawn() {
        GameObject newEnemy = Instantiate (enemy, transform.position + (Random.Range (-spawnXRange, spawnXRange) * transform.right), transform.rotation);
        Invoke ("Spawn", spawnDelay);
    }

    private void FixedUpdate() {
        spawnDelay -= Time.fixedDeltaTime / spawnDelayLoweringTime;
        if (spawnDelay < minSpawnDelay)
            spawnDelay = minSpawnDelay;
    }
}
