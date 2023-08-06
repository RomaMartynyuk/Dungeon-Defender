using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemyPrefab;
        public int[] count;
        public float rate;
    }

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] List<Wave> waves;
    [SerializeField] float timeBtwWaves;
    [SerializeField] TextMeshProUGUI numOfWaveUI;
    [SerializeField] GameObject winMenu;

    float countdown;
    int currWave;
    public int amountWaves;
    
    [SerializeField] int aliveEnemies;

    private void Start()
    {
        aliveEnemies = 0;
        amountWaves = waves.Count;
    }

    private void Update()
    {
        if (currWave == amountWaves && aliveEnemies == 0)
            Win();

        if (aliveEnemies > 0)
        {
            return;
        }
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBtwWaves;
            return;
        }

        countdown -= Time.deltaTime;
    }

    void Win()
    {
        numOfWaveUI.text = " ";
        winMenu.SetActive(true);
    }

    IEnumerator SpawnWave()
    {
        numOfWaveUI.text = GetCurrWave().ToString();

        Wave wave = waves[currWave];

        for (int i = 0; i < wave.count.Length; i++)
        {
            for (int j = 0; j < wave.count[i]; j++)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                SpawnEnemy(wave.enemyPrefab[i], spawnPointIndex);

                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        currWave++;
    }

    void SpawnEnemy(GameObject enemy, int spawnPoint)
    {
        Instantiate(enemy, spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
        aliveEnemies++;
    }

    public int GetCurrWave()
    {
        return currWave + 1;
    }

    public void DecreaseAliveEnemies()
    {
        aliveEnemies--;
    }

}
