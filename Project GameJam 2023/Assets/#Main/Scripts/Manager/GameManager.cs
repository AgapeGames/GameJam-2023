using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timerWave;
    public float timerWaveCounter;
    public float timerFinish;
    public bool isReadySpawn;

    public int minCountEnemy, maxCountEnemy;
    public GameObject prefabEnemy;
    public Transform[] listPositionSpawn;

    public int countEnemy;
    public List<Enemy> listEnemy;


    [Header("Object")]
    public Transform positionTree;
    public Transform positionPlayer;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        timerWaveCounter = timerWave;
        isReadySpawn = false;

        StartWave();
    }

    void Update()
    {
        if(isReadySpawn)
        {
            if(timerWaveCounter <= 0)
            {
                timerWaveCounter = timerWave;
                isReadySpawn = false;
                StartWave();
            }
            else
            {
                timerWaveCounter -= Time.deltaTime;
            }

            CanvasManager.Instance.textTimerWave.text = $"Next Wave : {(int)timerWaveCounter}s";
        }
    }

    public void StartWave()
    {
        countEnemy = Random.Range(minCountEnemy, maxCountEnemy);
        StartCoroutine(SpawnEnemy(1));

    }
    public void EnemyDie(Enemy currentEnemy)
    {
        listEnemy.Remove(currentEnemy);
        CheckEnemy();
    }
    public void CheckEnemy()
    {
        if (listEnemy.Count <= 0)
        {
            timerWaveCounter = timerFinish;
        }
    }

    IEnumerator SpawnEnemy(int currentCountEnemy)
    {
        GameObject newEnemy = Instantiate(prefabEnemy, listPositionSpawn[Random.Range(0, listPositionSpawn.Length)].position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().Init();
        listEnemy.Add(newEnemy.GetComponent<Enemy>());

        yield return new WaitForSeconds(2);

        currentCountEnemy++;
        if (currentCountEnemy < countEnemy)
            StartCoroutine(SpawnEnemy(currentCountEnemy));
        else
        {
            isReadySpawn = true;
        }
    }

}
