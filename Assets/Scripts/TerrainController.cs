using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    // Spawner stats
    public static int tilesArrayNumber = 1;
    [SerializeField]  public float maxTiles = 20f;
    private float tileLength = 1f;
    private static float tileSpawnZ = 0f;
    [SerializeField]  private static float tileSpawnY = -3f;
    private static float tileSpawnX = 0f;
    [SerializeField]  public float safeZone = 15f;
    public GameObject[] tiles = new GameObject[tilesArrayNumber];
    List<GameObject> activeTerrain = new List<GameObject>();
    private Vector3 spawnVector = new Vector3(tileSpawnX, tileSpawnY, tileSpawnZ);

    // Camera components
    float cameraWidth;
    private GameObject mainCamera;
    private Transform cameraTransform;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraWidth = Camera.main.orthographicSize * 2;
        safeZone += cameraWidth;
        tileSpawnX = cameraWidth * -1;
        spawnVector.x = tileSpawnX;
        for (int i = 0; i < maxTiles; i++)
        {
            SpawnTile();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deletePositionX = (mainCamera.transform.position.x - (cameraWidth / 2) - safeZone);
        if (activeTerrain[0].transform.position.x < deletePositionX)
        {
            DeleteTile();
            SpawnTile();
        }
    }

    void SpawnTile()
    {
        GameObject tile = Instantiate(tiles[0]) as GameObject;
        tile.transform.SetParent(this.transform);
        tile.transform.position = spawnVector;
        spawnVector.x += tileLength;
        activeTerrain.Add(tile);
    }

    void DeleteTile()
    {
        GameObject tile = activeTerrain[0];
        Destroy(tile);
        activeTerrain.RemoveAt(0);
    }
}
