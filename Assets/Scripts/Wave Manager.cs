using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int wave = 1;
    [SerializeField] private List<GameObject> enemyTypes = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    private Transform enemySpawn;

    [SerializeField] private float spawnDelay = 1f;
    private float initialSpawnDelay;

    private TextMeshProUGUI waveText;

    private GameObject player;

    [SerializeField] private GameObject healthPickup;
    private Vector2 healthPickupPos;

    private void Awake()
    {
        enemySpawn = GameObject.Find("Enemy Spawn").transform;
        waveText = GameObject.Find("Wave Text").GetComponent<TextMeshProUGUI>();
        initialSpawnDelay = spawnDelay;

        player = GameObject.Find("Player");
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        waveText.SetText("WAVE: " + wave);
        spawnDelay = initialSpawnDelay - (wave / 25);

        if (enemies.Count <= 0)
        {
            NewWave();
        }
    }

    private void NewWave()
    {
        wave += 1;

        healthPickupPos = new Vector2(Random.Range(0f, 8.8f), player.transform.position.y);

        Instantiate(healthPickup, healthPickupPos, Quaternion.identity);

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < wave; i++)
        {
            GameObject _enemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], enemySpawn);
            enemies.Add(_enemy);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void RemoveEnemy(GameObject _enemy)
    {
        enemies.Remove(_enemy);
        Destroy(_enemy);
    }
}
