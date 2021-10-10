using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Types;

public class PowerUpSpawnerScript : MonoBehaviour
{

    [SerializeField]  private float minPowerUpDistance;
    [SerializeField] private float maxPowerUpDistance;
    [SerializeField]  private float safeZone = 10f;
    Vector3 SpawnVector = new Vector3(0, 0, 0);
    public GameObject[] PrefabsArray = new GameObject[2];
    private List<GameObject> ActivePowerUpsList = new List<GameObject>();

    float cameraWidth;
    GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraWidth = Camera.main.orthographicSize * 2;
        minPowerUpDistance = cameraWidth * 1;
        maxPowerUpDistance = cameraWidth * 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void SpawnPowerUp(EPowerUps powerUp)
    {
        GameObject regen = Instantiate(PrefabsArray[(int)powerUp]) as GameObject;
        regen.transform.SetParent(this.transform);
        float distance = Random.Range(minPowerUpDistance, maxPowerUpDistance);

        SpawnVector.x = mainCamera.transform.position.x + distance;
        regen.transform.position = SpawnVector;
        ActivePowerUpsList.Add(regen);
    }

    void DeletePowerUp()
    {
        GameObject pu = ActivePowerUpsList[0];
        Destroy(pu);
        ActivePowerUpsList.RemoveAt(0);
    }
}
