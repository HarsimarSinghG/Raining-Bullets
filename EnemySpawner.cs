using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySO> listOfEnemies;
    [SerializeField] private Transform allEnemiesOnScreen;
    private float timerForEnemySpawn;
    private void Awake()
    {
        timerForEnemySpawn = 3f;
    }

    private void Update()
    {
        timerForEnemySpawn -= Time.deltaTime;
        if (timerForEnemySpawn < 0f)
        {
            SpawnEnemies();
            timerForEnemySpawn = Random.Range(1000,100000);
        }
    }

    private void SpawnEnemies()
    {

        EnemySO enemyTypeToSpawnSO = listOfEnemies[Random.Range(0, listOfEnemies.Count)];
        Transform enemyPrefab = enemyTypeToSpawnSO.enemy.GetComponent<Transform>();
        int randomNumberOfEnemies = Random.Range(0,0);
        Vector3 spawnPosition= ReturnPositionToSpawn();
        for (int i = 0; i < randomNumberOfEnemies; i++)
        {
            Transform enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<Enemy>().SetAllEnemiesOnScreen(allEnemiesOnScreen);
            enemy.GetComponent<Enemy>().SetSpeed(enemyTypeToSpawnSO.speed);
            enemy.GetComponent<Enemy>().SetHealth(enemyTypeToSpawnSO.health);
		    enemy.parent = allEnemiesOnScreen;
            Vector3 position = spawnPosition;
            if (position != Vector3.zero) { }
            enemy.localPosition = position;
        }
    }

    private Vector3 ReturnPositionToSpawn() {
        float randomX = 0;
        float randomY = 0;
        int positionToSpawn = 5;
        int randomOrFront = Random.Range(0, 2);
        if (randomOrFront == 0)
        {
            Vector3 playerFacingDirection = Player.Instance.ReturnMoveDir();
            if (playerFacingDirection == Vector3.zero)
            { positionToSpawn = Random.Range(0, 4); }
            else
            {
                if (playerFacingDirection.x > 0) { randomX = 20; }
                if (playerFacingDirection.x < 0) { randomX = -20; }
                if (playerFacingDirection.z > 0) { randomY = 20; }
                if (playerFacingDirection.z < 0) { randomY = -20; }

            }
        }
        if(randomOrFront==1 || positionToSpawn != 5)
        {
            positionToSpawn = Random.Range(0, 4);
            if (positionToSpawn == 0)
            // 0 is top-right corner.
            {
                randomX = Random.Range(20, 30);
                randomY = Random.Range(20, 30);
            }
            else
            {
                if (positionToSpawn == 1)
                // 1 is top-left corner.
                {
                    randomX = Random.Range(-30, -20);
                    randomY = Random.Range(20, 30);
                }
                else
                {
                    if (positionToSpawn == 2)
                    // 2 is bottom-right corner.
                    {
                        randomX = Random.Range(20, 30);
                        randomY = Random.Range(-30, -20);
                    }
                    else
                    {
                        if (positionToSpawn == 3)
                        // 3 is bottom-left corner.
                        {
                            randomX = Random.Range(-30, -20);
                            randomY = Random.Range(-30, -20);
                        }
                    }
                }
            }
        }
        if (Player.Instance != null)
        {
            Vector3 position = new Vector3(Player.Instance.transform.position.x + randomX, Player.Instance.transform.position.y + randomY, Player.Instance.transform.position.z);
            return position;
        }
        else { return Vector3.zero; }
    }
}
