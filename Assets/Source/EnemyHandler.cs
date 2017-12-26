using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyHandler : MonoBehaviour {

    public static EnemyHandler enemyHandler;

    public EnemyType[] enemies;
    public Queue<GameObject> enemyQueue = new Queue<GameObject> ();

    public float maxSpawnDelay;
    public float minSpawnDelay;
    public AnimationCurve spawnDelayCurve;
    public int totalPredefinedWaves = 100;

    public static bool waveStarted = false;
    public static int WaveCount { get { return enemyHandler.wave; } set { enemyHandler.wave = value; } }
    public int wave;

    public float spawnXRange = 20;
    public int spawnPerWave = 5;
    public int spawnAmount;

    public delegate void WaveEventHandler ();
    public static event WaveEventHandler OnWaveEnded;
    public static event WaveEventHandler OnWaveStarted;

    // Use this for initialization
    private void Awake() {
        enemyHandler = this;
    }

    private void Start() {
        EndWave ();
    }

    void SpawnNext() {
        GameObject newEnemy = Instantiate (enemyQueue.Dequeue (), transform.position + (UnityEngine.Random.Range (-spawnXRange, spawnXRange) * transform.right), transform.rotation);
        spawnAmount--;

        if (enemyQueue.Count > 0) {
            Invoke ("SpawnNext", GetSpawnDelay (WaveCount));
        } else
            EndWave ();
    }

    public static void StaticStartWave () {
        enemyHandler.StartWave ();
    }

    public void StartWave() {
        waveStarted = true;
        WaveCount++;
        spawnAmount = GetSpawnAmount (WaveCount);

        PopulateEnemyQueue ();
        SpawnNext ();

        WaveEventHandler handler = OnWaveStarted;
        if (handler != null) {
            handler ();
        }
    }

    private void PopulateEnemyQueue() {
        PopulateEnemyQueue (CalculateSpawnAmount (WaveCount, spawnAmount));
    }

    private void PopulateEnemyQueue (int[] toSpawn) {
        List<GameObject> list = new List<GameObject> ();

        for (int i = 0; i < toSpawn.Length; i++) {
            int amount = toSpawn [ i ];
            EnemyType enemy = enemies [ i ];

            for (int j = 0; j < amount; j++) {
                list.Add (enemy.enemy);
            }
        }
        
        // Shuffle that shiznat.
        while (list.Count != 0) {
            int position = UnityEngine.Random.Range (0, list.Count);
            enemyQueue.Enqueue (list [position]);
            list.RemoveAt (position);
        }
    }

    public void EndWave() {
        waveStarted = false;

        WaveEventHandler handler = OnWaveEnded;
        if (handler != null) {
            handler ();
        }
    }

    public int[] CalculateSpawnAmount (int wave, int total) {

        int [ ] spawnAmount = new int [ enemies.Length ];
        float totalPercentage = 0f; // This termonology doesn't make sense, just act like it does.

        foreach (EnemyType enemy in enemies) {
            totalPercentage += enemy.GetPercentageAtWave (wave);
        }

        for (int i = 0; i < enemies.Length; i++) {
            float normalized = enemies[i].GetPercentageAtWave (wave) / totalPercentage;
            spawnAmount [ i ] = Mathf.RoundToInt (normalized * total);
        }

        return spawnAmount;
    }

    public EnemyType GetEnemyType (int index) {
        return enemies [ index ];
    }

    private static float GetGameProgress() {
        return WaveCount / (float)enemyHandler.totalPredefinedWaves;
    }

    private static float GetGameProgress (int wave) {
        return wave / (float)enemyHandler.totalPredefinedWaves;
    }

    public static int GetSpawnAmount (int wave) {
        return enemyHandler.spawnPerWave * wave;
    }

    public static float GetSpawnDelay (int wave) {
        float delta = enemyHandler.maxSpawnDelay - enemyHandler.minSpawnDelay;
        return enemyHandler.minSpawnDelay + delta * enemyHandler.spawnDelayCurve.Evaluate (GetGameProgress (wave));
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube (transform.position, Vector3.one + Vector3.right * spawnXRange);
    }

    [Serializable]
    public class EnemyType {
        public GameObject enemy;
        public int startingWave;
        public float spawnPercentage;
        public AnimationCurve spawnPercentageModifier;

        public float GetPercentageAtWave (int wave) {
            if (startingWave <= wave) {
                return spawnPercentage * spawnPercentageModifier.Evaluate (GetGameProgress (wave));
            } else {
                return 0f;
            }
        }
    }
}
