using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    public GameObject[] enemyPrefabs;
    public GameObject[] powerUpPrefabs;
    
    public int enemyCount;
    public int waveNumber = 1;

    
    private float _spawnPositionRange = 9;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        SpawnEnemyWave(waveNumber);
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update() {
        enemyCount = GetEnemyCount();
        if (enemyCount == 0) {
            waveNumber++;
            SpawnPowerUp();
            SpawnEnemyWave(waveNumber);
        }
    }
    
    private void SpawnPowerUp() {
        int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(
            powerUpPrefabs[randomPowerUp],
            GenerateSpawnPosition(),
            powerUpPrefabs[randomPowerUp].transform.rotation);
    }

    private int GetEnemyCount() {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
    }

    private Vector3 GenerateSpawnPosition() {
        var rangePosX = Random.Range(-_spawnPositionRange, _spawnPositionRange);
        var rangePosZ = Random.Range(-_spawnPositionRange, _spawnPositionRange);
        
        return new Vector3(rangePosX, 0, rangePosZ);
    }

    void SpawnEnemyWave(int enemiesToSpawn) {
        for (int i = 0; i < enemiesToSpawn; i++) {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            
            Instantiate(
                enemyPrefabs[randomEnemy],
                GenerateSpawnPosition(),
                enemyPrefabs[randomEnemy].transform.rotation
                );
        }
    }
}