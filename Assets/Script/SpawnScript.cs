using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject CoinTossPrefab;
    public Transform[] spawnPoints; // Drag multiple positions here

    void Start()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(CoinTossPrefab, point.position, point.rotation);
        }
    }
}
