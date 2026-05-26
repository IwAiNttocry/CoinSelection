using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CoinSelection : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private CoinBehaviour _hoveredPiece;
    private List<CoinBehaviour> _selectedPieces = new List<CoinBehaviour>();
    private Vector2Int _lastSquare = -Vector2Int.one;
    private Vector2 _lastMousePos;

    void Update()
    {
        if (Discard.IsOnCooldown) return;
        CheckHover();
        CheckClick();
    }

    void CheckHover()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (Vector2.Distance(mousePos, _lastMousePos) < 5f) return;
        _lastMousePos = mousePos;

        Vector2Int currentSquare = GetSquareUnderMouse();
        if (currentSquare == _lastSquare) return;
        _lastSquare = currentSquare;

        CoinBehaviour hit = GetPieceUnderMouse();

        if (hit != null && _hoveredPiece == null && !_selectedPieces.Contains(hit))
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

    void CheckClick()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            CoinBehaviour rightHit = GetPieceUnderMouse();
            if (rightHit != null && _selectedPieces.Contains(rightHit))
            {
                rightHit.OnDeselect();
                _selectedPieces.Remove(rightHit);
            }
            return;
        }

        if (!Mouse.current.leftButton.wasPressedThisFrame) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        CoinBehaviour hit = GetPieceUnderMouse();

        if (hit != null)
        {
            if (_selectedPieces.Contains(hit))
            {
                hit.OnDeselect();
                _selectedPieces.Remove(hit);
            }
            else
            {
                bool wasHovered = (_hoveredPiece == hit);
                _selectedPieces.Add(hit);
                hit.OnSelect(wasHovered);
                _hoveredPiece = null;
            }
        }
    }

    public List<CoinBehaviour> GetSelectedPieces() => _selectedPieces;

    public void ClearHovered() { _hoveredPiece = null; }

    Vector2Int GetSquareUnderMouse()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
            return new Vector2Int(Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.z));
        return -Vector2Int.one;
    }

    CoinBehaviour GetPieceUnderMouse()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        foreach (RaycastHit hit in Physics.RaycastAll(ray, Mathf.Infinity))
        {
            CoinBehaviour piece = hit.transform.GetComponent<CoinBehaviour>();
            if (piece != null) return piece;
        }
        return null;
    }
}