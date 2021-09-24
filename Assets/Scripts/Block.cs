using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public delegate bool DragEndedDelegate(Block block);
    public DragEndedDelegate snapCompleted;

    public Cell[] Cells;
    private float mouseStartPosX;
    private float mouseStartPosY;

    [HideInInspector]
    public Vector3 PositionInHolder;
    // private float startPosX;
    // private float startPosY;

    private bool isBeingHeld = false;
    private bool isDraggable = true;

    private Vector3 blockInitialScale;
    private Vector3 cellInitialScale;

    private void Awake()
    {
        blockInitialScale = transform.localScale;
        cellInitialScale = Cells[0].transform.localScale;
    }

    private void Start()
    {
        snapCompleted += SnapController.Instance.OnDragEnded;
        // startPosX = transform.position.x;
        // startPosY = transform.position.y;
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
            transform.DOScale(2f * blockInitialScale, 0.2f);
            foreach (Cell cell in Cells)
            {
                cell.transform.DOScale(0.8f * cellInitialScale, 0.2f);
            }

            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        if (isBeingHeld)
        {
            isBeingHeld = false;

            //check if all cells can fit then snap
            bool isSnapCompleted = snapCompleted(this);

            if (isSnapCompleted)
            {
                isDraggable = false;

                //Snap animation
                foreach (Cell cell in Cells)
                {
                    cell.transform.DOScale(cellInitialScale, 0f);
                }
                BlocksHolder.Instance.UpdateHolder();
            }
            else
            {
                //Restore animation
                transform.DOScale(blockInitialScale, 0.2f);
                foreach (Cell cell in Cells)
                {
                    cell.transform.DOScale(cellInitialScale, 0.2f);
                }
                transform.DOMove(PositionInHolder, 0.2f);
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
