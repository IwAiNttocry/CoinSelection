using UnityEngine;

public class Regen : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    public void OnRespawn()
    {
        foreach (int index in Discard.EmptySpawnIndices)
        {
            Instantiate(_coinPrefab, _spawnPoints[index].position, _spawnPoints[index].rotation);
        }
        Discard.EmptySpawnIndices.Clear();
    }
}