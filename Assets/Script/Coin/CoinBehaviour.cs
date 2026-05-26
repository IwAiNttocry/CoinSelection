using UnityEngine;
using DG.Tweening;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CoinSelection _selector;

    private static readonly Vector3 LiftOffset = Vector3.up * 0.5f;
    private Vector3 _basePosition;

    void Start()
    {
        _basePosition = transform.position;
    }

    public void OnHoverEnter()
    {
        transform.DOKill();
        transform.DOMove(_basePosition + LiftOffset, 0.3f).SetEase(Ease.InOutSine);
    }

    public void OnHoverExit()
    {
        transform.DOKill();
        transform.DOMove(_basePosition, 0.3f).SetEase(Ease.InOutSine);
    }

    public void OnSelect(bool alreadyLifted)
    {
        transform.DOKill();
        transform.DOMove(_basePosition + LiftOffset, 0.35f).SetEase(Ease.InOutCubic);
    }

    public void OnDeselect()
    {
        transform.DOKill();
        transform.DOMove(_basePosition, 0.35f).SetEase(Ease.InOutCubic);
    }

    public void UpdateBasePosition(Vector3 newBase)
    {
        _basePosition = newBase;
    }
}