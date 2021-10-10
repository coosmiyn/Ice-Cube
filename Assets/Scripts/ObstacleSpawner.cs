using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObstacleSpawner : MonoBehaviour
{
    // ObstacleSpawner Components
    [SerializeField] private int minSpawnDistance = 5;
    [SerializeField] private int maxSpawnDistance = 10;
    [SerializeField] private int maxObstacles = 10;
    [SerializeField] private float safeZone = 10f;
    [SerializeField] private static float spawnX = 0;
    [SerializeField] private static float spawnY = -2.075f;
    [SerializeField] private static float spawnZ = 0;
    private static int arrayLength = 2;
    Vector3 SpawnVector = new Vector3(spawnX, spawnY, spawnZ);
    public GameObject[] PrefabsArray = new GameObject[arrayLength];
    private List<GameObject> ActiveObstaclesList = new List<GameObject>();
    private List<List<GameObject>> ClampedObstacles = new List<List<GameObject>>();

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
            SpawnSpike();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Obstacles in scene manager
        float deletePosition = (cameraTransform.position.x - (cameraWidth / 2) - safeZone);
        if (ClampedObstacles[0][0].transform.position.x < deletePosition)
        {
            DeleteObstacle();
            SpawnSpike();
        }
    }


    private void DeleteObstacle()
    {
        //GameObject obstacle = ActiveObstaclesList[0];
        //Destroy(obstacle);
        for (int i = 0; i < ClampedObstacles[0].Count; i++)
        {
            GameObject obs = ClampedObstacles[0][i];
            Destroy(obs);
        }
        ClampedObstacles.RemoveAt(0);
    }

    private void SpawnSpike()
    {
        int objectToSpawn = (int)Random.Range(0, 2);
        float spikeWidth = PrefabsArray[objectToSpawn].GetComponent<SpriteRenderer>().bounds.size.x;
        int numberOfSpikes = (int)Random.Range(2, 6);
        List<GameObject> gameObjects = new List<GameObject>();
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        SpawnVector.x += distance;

        for (int i = 0; i < numberOfSpikes; i++)
        {
            objectToSpawn = Mathf.Clamp(objectToSpawn, 0, 2);
            GameObject obstacle = Instantiate(PrefabsArray[objectToSpawn]) as GameObject;
            obstacle.transform.parent = this.transform.parent;
            SpawnVector.x += spikeWidth / 2;
            obstacle.transform.position = SpawnVector;
            gameObjects.Add(obstacle);
        }

        ClampedObstacles.Add(gameObjects);
    }
    private void SpawnObstacle()
    {
        // Create and spawn object
        GameObject obstacle = Instantiate(PrefabsArray[Random.Range(0, arrayLength - 1)]) as GameObject;
        obstacle.transform.parent = this.transform.parent;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        SpawnVector.x += distance;
        obstacle.transform.position = SpawnVector;
        ActiveObstaclesList.Add(obstacle);

        //// Check if collides with regen
        //Collider2D[] colliders = { new Collider2D() };
        //Vector3 checkSpawnCollision = SpawnVector;
        //checkSpawnCollision.x += distance;
        //int count = Physics2D.OverlapCircleNonAlloc(checkSpawnCollision, 3.0f, colliders);

        //// If collides, destroy and spawn again 1 unit further
        //if (count > 0)
        //{
        //    SpawnVector.x += 1.0f;
        //    Destroy(obstacle);
        //    SpawnObstacle();
        //    Debug.Log("Repairing spawn");
        //}
        //else
        //{
        //    SpawnVector.x += distance;
        //    obstacle.transform.position = SpawnVector;
        //    ActiveObstaclesList.Add(obstacle);
        //}
    }
}
