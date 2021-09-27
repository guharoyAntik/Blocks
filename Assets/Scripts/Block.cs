using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Block : MonoBehaviour
{
    public delegate Task DragEndedDelegate(Block block);

    public DragEndedDelegate dragEnded;

    public Cell[] Cells;
    private float mouseStartPosX;
    private float mouseStartPosY;

    [HideInInspector]
    public Vector3 PositionInHolder;

    private bool isBeingHeld = false;
    private bool isDraggable = true;

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
        dragEnded += SnapController.Instance.OnDragEnded;
    }

    private void Update()
    {
        if (isBeingHeld == true)
        {
            Vector3 mousePos = GetMousePosition();

            transform.position = new Vector3(mousePos.x - mouseStartPosX, mousePos.y - mouseStartPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && isDraggable)
        {
            Vector3 mousePos = GetMousePosition();

            mouseStartPosX = mousePos.x - transform.position.x;
            mouseStartPosY = mousePos.y - transform.position.y;

            //Animate selection
            transform.DOScale(2f * BlockInitialScale, 0.2f);
            foreach (Cell cell in Cells)
            {
                cell.transform.DOScale(0.8f * CellInitialScale, 0.2f);
            }

            isBeingHeld = true;
        }
    }

    private async void OnMouseUp()
    {
        if (isBeingHeld)
        {
            isBeingHeld = false;
            await dragEnded(this);
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
}
