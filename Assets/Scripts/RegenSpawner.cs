using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RegenSpawner : MonoBehaviour
{
    // Spawner stats
    private int maxRegens = 5;
    private float minRegenDistance;
    private float maxRegenDistance;
    private float safeZone = 10f;

    Vector3 SpawnVector = new Vector3(0, -1.5f, 0);

    public GameObject[] PrefabsArray = new GameObject[1];

    private List<GameObject> ActiveRegenList = new List<GameObject>();

    // Camera components
    float cameraWidth;
    GameObject mainCamera;
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        //yield return new WaitForSeconds(1.0f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTransform = mainCamera.GetComponent<Transform>();
        cameraWidth = Camera.main.orthographicSize * 2;
        minRegenDistance = cameraWidth * 2;
        maxRegenDistance = cameraWidth * 4;

        for (int i = 0; i < maxRegens; i++)
        {
            SpawnRegen();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float deletePosition = (mainCamera.transform.position.x - (cameraWidth / 2) - safeZone);
        if (ActiveRegenList[0].transform.position.x < deletePosition)
        {
            DeleteRegen();
            SpawnRegen();
        }
    }

    void SpawnRegen()
    {
        GameObject regen = Instantiate(PrefabsArray[0]) as GameObject;
        regen.transform.SetParent(this.transform);
        float distance = Random.Range(minRegenDistance, maxRegenDistance);

        // Check if collides with regen
        Collider2D[] colliders = { new Collider2D() };
        Vector3 checkSpawnCollision = SpawnVector;
        checkSpawnCollision.x += distance;
        int count = Physics2D.OverlapCircleNonAlloc(checkSpawnCollision, 3.0f, colliders);

        // If collides, destroy and spawn again 1 unit further
        if (count > 0)
        {
            SpawnVector.x += 1.0f;
            Destroy(regen);
            SpawnRegen();
        }
        else
        {
            SpawnVector.x += distance;
            regen.transform.position = SpawnVector;
            ActiveRegenList.Add(regen);
        }

        //SpawnVector.x += distance;
        //regen.transform.position = SpawnVector;
        //ActiveRegenList.Add(regen);
    }

    void DeleteRegen()
    {
        GameObject regen = ActiveRegenList[0];
        Destroy(regen);
        ActiveRegenList.RemoveAt(0);
    }
}
