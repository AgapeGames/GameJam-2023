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
    public int[] minCountEnemyLevel, maxCountEnemylevel;
    public GameObject prefabEnemy;
    public Transform[] listPositionSpawn;

    public int countEnemy;
    public List<Enemy> listEnemy;


    [Header("Object")]
    public Transform positionTree;
    public Transform positionPlayer;


    public PlayerControl playerControl;
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
                isReadySpawn = false;
                StartWave();
            }
            else
            {
                timerWaveCounter -= Time.deltaTime;
            }

            CanvasManager.Instance.textTimerWave.text = $"Next Wave : {(int)timerWaveCounter}s";
        }

        SpawnItems();
    }

    public void StartWave()
    {

        timerWaveCounter = timerWave + TreeControl.Instance.level * 30;
        countEnemy = Random.Range(
            minCountEnemyLevel[TreeControl.Instance.level - 1], 
            maxCountEnemylevel[TreeControl.Instance.level - 1]);

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


    public void GameWin()
    {
        PlayerFreeze();
        CanvasManager.Instance.panelWin.SetActive(true);
    }
    public void GameLose()
    {
        PlayerFreeze();
        CanvasManager.Instance.panelLose.SetActive(true);
    }

    public void PlayerFreeze()
    {
        playerControl.isActive = false;
    }
    public void PlayerUnfreeze()
    {
        playerControl.isActive = true;
    }

    [Header("Items Spawns")]
    public float timeSpawns;
    private float timeSpawnsCounter;
    public int maxCountItems;

    public LayerMask obstacleLayer;
    public List<int> listItems;

    public GameObject objItemScraps;
    public GameObject objItemBattery;

    public float minXPosSpawn;
    public float maxXPosSpawn;
    public float minYPosSpawn;
    public float maxYPosSpawn;

    public void SpawnItems()
    {
        timeSpawnsCounter -= Time.deltaTime;
        if(timeSpawnsCounter <= 0)
        {
            timeSpawnsCounter = timeSpawns;
            SpawnRandomItem();
        }
    }
    private void SpawnRandomItem()
    {
        if (listItems.Count >= maxCountItems) return;

        Vector3 randomSpawnPoint = GetRandomSpawnPoint();

        // Periksa apakah ada objek di lokasi spawn
        if (!IsObstacleInPath(randomSpawnPoint))
        {
            if (Random.Range(-1, 100f) > 30)
            {
                GameObject objS = Instantiate(objItemScraps, randomSpawnPoint, Quaternion.identity);
                objS.GetComponent<Item>().Spawn();
            }
            else
            {
                GameObject objB = Instantiate(objItemBattery, randomSpawnPoint, Quaternion.identity);
                objB.GetComponent<Item>().Spawn();
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(minXPosSpawn, maxXPosSpawn);
        float randomY = Random.Range(minYPosSpawn,maxYPosSpawn);

        return transform.position + new Vector3(randomX, randomY, 0);
    }

    private bool IsObstacleInPath(Vector3 spawnPoint)
    {
        RaycastHit hit;

        // Mengecek apakah ada collider dalam radius kecil di sekitar spawnPoint
        if (Physics.SphereCast(spawnPoint, 0.5f, Vector3.up, out hit, 0.1f, obstacleLayer))
        {
            return true; // Ada objek penghalang di dekat spawnPoint
        }

        return false; // Tidak ada objek penghalang di dekat spawnPoint
    }
}
