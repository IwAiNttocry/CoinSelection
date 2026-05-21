using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CoinSelection selctor;

    private static readonly Vector3 LiftOffset = Vector3.up * 0.5f;

    
    public void OnHoverEnter()
    {
        transform.position += LiftOffset;
    }

    
    public void OnHoverExit()
    {
        transform.position -= LiftOffset;
    }

    
    public void OnSelect(bool alreadyLifted)
    {
        if (!alreadyLifted){transform.position += LiftOffset;}            
    }

        public void OnDeselect()
    {
        transform.position -= LiftOffset; 
    }

}