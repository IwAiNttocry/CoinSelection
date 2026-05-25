using UnityEngine;
using System.Collections.Generic;

public class Discard : MonoBehaviour
{
    [SerializeField] private CoinSelection _coinSelection;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    public static List<int> EmptySpawnIndices = new List<int>();

    public void OnDiscard()
    {
        foreach (CoinBehaviour coin in _coinSelection.GetSelectedPieces())
        {
            if (coin == null) continue;
            int closest = GetClosestSpawnIndex(coin.transform.position);
            if (!EmptySpawnIndices.Contains(closest))
                EmptySpawnIndices.Add(closest);
            Destroy(coin.gameObject);
        }

        _coinSelection.GetSelectedPieces().Clear();
        _coinSelection.ClearHovered();
    }

    private int GetClosestSpawnIndex(Vector3 pos)
    {
        int closest = 0;
        float minDist = float.MaxValue;
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            float dist = Vector3.Distance(pos, _spawnPoints[i].position);
            if (dist < minDist) { minDist = dist; closest = i; }
        }
        return closest;
    }
}