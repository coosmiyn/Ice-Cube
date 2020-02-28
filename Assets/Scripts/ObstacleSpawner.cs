using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // ObstacleSpawner Components
    private int minSpawnDistance = 5;
    private int maxSpawnDistance = 10;
    private static int ArrayLength = 6;
    private int maxObstacles = 10;
    private float safeZone = 10f;
    private static float spawnX = 0;
    private static float spawnY = -2.28f;
    private static float spawnZ = 0;

    private float deletePosition;

    Vector3 SpawnVector = new Vector3(spawnX, spawnY, spawnZ);

    public GameObject[] PrefabsArray = new GameObject[ArrayLength];
    private List<GameObject> ActiveObstaclesList = new List<GameObject>();

    //Camera Components
    float cameraWidth;
    GameObject mainCamera;
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = mainCamera.GetComponent<Transform>();
        cameraWidth = Camera.main.orthographicSize * 2;
        for (int i = 0; i < maxObstacles; i++)
        {
            SpawnObstacle();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deletePosition = (cameraTransform.position.x - (cameraWidth / 2) - safeZone);
        if (ActiveObstaclesList[0].transform.position.x < deletePosition)
        {
            DeleteObstacle();
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(GetObstacleToSpawn()) as GameObject;
        obstacle.transform.parent = this.transform.parent;
        SpawnVector.x += GetObstacleDistance();
        obstacle.transform.position = SpawnVector;
        ActiveObstaclesList.Add(obstacle);
    }

    private void DeleteObstacle()
    {
        GameObject obstacle = ActiveObstaclesList[0];
        Destroy(obstacle);
        ActiveObstaclesList.RemoveAt(0);
    }

    private Object GetObstacleToSpawn()
    {
        return PrefabsArray[Random.Range(0, ArrayLength - 1)];
    }

    private float GetObstacleDistance()
    {
        return Random.Range(minSpawnDistance, maxSpawnDistance);
    }
}
