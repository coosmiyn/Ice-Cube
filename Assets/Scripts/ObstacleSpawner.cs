using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObstacleSpawner : MonoBehaviour
{
    // ObstacleSpawner Components
    [SerializeField]
    private int minSpawnDistance = 5;
    [SerializeField]
    private int maxSpawnDistance = 10;
    private static int arrayLength = 6;
    [SerializeField]
    private int maxObstacles = 10;
    [SerializeField]
    private float safeZone = 10f;
    [SerializeField]
    private static float spawnX = 0;
    [SerializeField]
    private static float spawnY = -2.28f;
    [SerializeField]
    private static float spawnZ = 0;

    Vector3 SpawnVector = new Vector3(spawnX, spawnY, spawnZ);

    public GameObject[] PrefabsArray = new GameObject[arrayLength];
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
        // Obstacles in scene manager
        float deletePosition = (cameraTransform.position.x - (cameraWidth / 2) - safeZone);
        if (ActiveObstaclesList[0].transform.position.x < deletePosition)
        {
            DeleteObstacle();
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        // Create and spawn object
        GameObject obstacle = Instantiate(PrefabsArray[Random.Range(0, arrayLength - 1)]) as GameObject;
        obstacle.transform.parent = this.transform.parent;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        // Check if collides with regen
        Collider2D[] colliders = { new Collider2D() };
        Vector3 checkSpawnCollision = SpawnVector;
        checkSpawnCollision.x += distance;
        int count = Physics2D.OverlapCircleNonAlloc(checkSpawnCollision, 3.0f, colliders);

        // If collides, destroy and spawn again 1 unit further
        if (count > 0)
        {
            SpawnVector.x += 1.0f;
            Destroy(obstacle);
            SpawnObstacle();
            Debug.Log("Repairing spawn");
        }
        else
        {
            SpawnVector.x += distance;
            obstacle.transform.position = SpawnVector;
            ActiveObstaclesList.Add(obstacle);
        }
    }

    private void DeleteObstacle()
    {
        GameObject obstacle = ActiveObstaclesList[0];
        Destroy(obstacle);
        ActiveObstaclesList.RemoveAt(0);
    }
}
