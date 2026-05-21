using UnityEngine;
using UnityEngine.EventSystems;

public class CoinSelection : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private CoinBehaviour _hoveredPiece;
    private CoinBehaviour _selectedPiece;
    private Vector2Int _lastSquare = -Vector2Int.one;
    private Vector3 _lastMousePos;

    void Update()
    {
        CheckHover();
        CheckClick();
    }

    // ─── Hover ────────────────────────────────────────────────────────────────

    void CheckHover()
    {
        if (_selectedPiece != null) return;
        if (Vector3.Distance(Input.mousePosition, _lastMousePos) < 5f) return;

        _lastMousePos = Input.mousePosition;

        Vector2Int currentSquare = GetSquareUnderMouse();
        if (currentSquare == _lastSquare) return;
        _lastSquare = currentSquare;

        CoinBehaviour hit = GetPieceUnderMouse();

        if (hit != null && _hoveredPiece == null)
        {
            _hoveredPiece = hit;
            _hoveredPiece.OnHoverEnter();
        }

        if (hit == null && _hoveredPiece != null)
        {
            _hoveredPiece.OnHoverExit();
            _hoveredPiece = null;
        }
    }

    // ─── Click ────────────────────────────────────────────────────────────────

    void CheckClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_selectedPiece != null)
            {
                _selectedPiece.OnDeselect();
                _selectedPiece = null;
            }
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        CoinBehaviour hit = GetPieceUnderMouse();
    }


    // ─── Raycasts ─────────────────────────────────────────────────────────────

    Vector2Int GetSquareUnderMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return new Vector2Int(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.z));
        return -Vector2Int.one;
    }

    CoinBehaviour GetPieceUnderMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        foreach (RaycastHit hit in Physics.RaycastAll(ray, Mathf.Infinity))
        {
            CoinBehaviour piece = hit.transform.GetComponent<CoinBehaviour>();
            if (piece != null) return piece;
        }
        return null;
    }
}
