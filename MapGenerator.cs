using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] GameObject floorPrefab;
    private float checkRadius = 1f;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Transform allFloors;
    private float timerBeforeNextMap = 0.2f;
    [SerializeField] private GameObject randomChest;

    private void Update()
    {
        timerBeforeNextMap -= Time.deltaTime;
        if (timerBeforeNextMap < 0f) { MapSpawner();
            timerBeforeNextMap = 0.2f; }

	}

    private void Start()
    {
        GameObject spawnedFloor = Instantiate(floorPrefab, allFloors);
        spawnedFloor.transform.localPosition = Vector3.zero;

    }

    private void MapSpawner()
    {
        Vector3 playerMoveDir = Player.Instance.ReturnMoveDir();
        float spawnX = 0f;
        float spawnY = 0f;
        if (playerMoveDir.x > 0) { spawnX = 100; }
        if (playerMoveDir.x < 0) { spawnX = -100; }
        if (playerMoveDir.y > 0) { spawnY = 100;  }
        if (playerMoveDir.y < 0) { spawnY = -100;  }

        GameObject currentFloor = Player.Instance.ReturnCurrentFloor();
        Collider[] checkExistingFloor = Physics.OverlapSphere(currentFloor.transform.position + new Vector3(spawnX, spawnY, 0), checkRadius, floorLayer, QueryTriggerInteraction.Collide);
        if (checkExistingFloor.Length == 0)
        {
            GameObject spawnedFloor = Instantiate(floorPrefab, allFloors);
            Transform objectSpawnPoints = spawnedFloor.GetComponent<Floor>().ReturnObjectSpawnPoints();
            foreach (Transform point in objectSpawnPoints)
            {
                int rand = Random.Range(0, 2);
                if (rand == 1) { GameObject chest = Instantiate(randomChest, point);
                    chest.transform.localPosition = Vector3.zero;
		 }
            }
            spawnedFloor.transform.localPosition = currentFloor.transform.position + new Vector3(spawnX, spawnY, 0);
        }

    }


    
}
