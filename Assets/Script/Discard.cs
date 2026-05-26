using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Discard : MonoBehaviour
{
    [SerializeField] private CoinSelection _coinSelection;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float _spawnDelay = 2f;

    public static bool IsOnCooldown = false;

    public void OnDiscard()
    {
        if (IsOnCooldown) return;

        List<CoinBehaviour> selected = _coinSelection.GetSelectedPieces();
        if (selected.Count == 0) return;

        IsOnCooldown = true;

        foreach (CoinBehaviour coin in selected)
        {
            if (coin == null) continue;

            Vector3 spawnPos = coin.transform.position - Vector3.up * 0.5f;

            // Shrink and destroy
            coin.transform.DOKill();
            coin.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    Destroy(coin.gameObject);

                    // Spawn replacement after delay
                    DOVirtual.DelayedCall(_spawnDelay, () =>
                    {
                        GameObject newCoin = Instantiate(_coinPrefab, spawnPos, Quaternion.identity);
                        newCoin.transform.localScale = Vector3.zero;
                        newCoin.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                    });
                });
        }

        selected.Clear();
        _coinSelection.ClearHovered();

        DOVirtual.DelayedCall(_spawnDelay + 0.5f, () => IsOnCooldown = false);
    }
}