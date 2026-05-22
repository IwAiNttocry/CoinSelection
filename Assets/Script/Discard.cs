using UnityEngine;

public class Discard : MonoBehaviour
{
    [SerializeField] private CoinSelection _coinSelection;

    public void OnDiscard()
    {
        Debug.Log("Discard");

        foreach (CoinBehaviour coin in _coinSelection.GetSelectedPieces())
        {
            if (coin != null)
                Destroy(coin.gameObject);
        }

        _coinSelection.GetSelectedPieces().Clear();
        _coinSelection.ClearHovered();
    }
}