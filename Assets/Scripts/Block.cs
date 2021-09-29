using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Block : MonoBehaviour
{
    public delegate Task DragEndedDelegate(Block block);

    public DragEndedDelegate DragEnded;

    public Cell[] Cells;
    private float _mouseStartPosX;
    private float _mouseStartPosY;

    [HideInInspector]
    public Vector3 PositionInHolder;

    private bool _isBeingHeld = false;
    private bool _isDraggable = true;
    private bool _isEnabled = true;

    [HideInInspector]
    public Vector3 BlockInitialScale;
    [HideInInspector]
    public Vector3 CellInitialScale;

    private void Awake()
    {
        BlockInitialScale = transform.localScale;
        CellInitialScale = Cells[0].transform.localScale;
    }

    private void Start()
    {
        DragEnded += SnapController.Instance.OnDragEnded;
    }

    private void Update()
    {
        if (_isBeingHeld == true)
        {
            Vector3 mousePos = GetMousePosition();

            transform.position = new Vector3(mousePos.x - _mouseStartPosX, mousePos.y - _mouseStartPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && _isDraggable && _isEnabled)
        {
            Vector3 mousePos = GetMousePosition();

            _mouseStartPosX = mousePos.x - transform.position.x;
            _mouseStartPosY = mousePos.y - transform.position.y;

            //Animate selection
            transform.DOScale(2f * BlockInitialScale, 0.2f);
            foreach (Cell cell in Cells)
            {
                cell.transform.DOScale(0.8f * CellInitialScale, 0.2f);
            }

            _isBeingHeld = true;
        }
    }

    private async void OnMouseUp()
    {
        if (_isBeingHeld && _isEnabled)
        {
            _isBeingHeld = false;
            await DragEnded(this);
            if (transform.position != PositionInHolder)
            {
                Board.Instance.CheckAndClear();
                Destroy(this.gameObject);
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos;

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

    #region Utility Methods
    public void EnableBlock()
    {
        _isEnabled = true;
    }

    public void DisableBlock()
    {
        _isEnabled = false;
    }

    #endregion
}