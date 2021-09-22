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

    private float startPosX;
    private float startPosY;

    private bool isBeingHeld = false;
    private bool isDraggable = true;

    private void Awake()
    {

    }

    private void Start()
    {
        snapCompleted += SnapController.Instance.OnDragEnded;
        startPosX = transform.position.x;
        startPosY = transform.position.y;
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
            transform.DOScale(new Vector3(2f, 2f, 2f), 0.5f);
            foreach (Cell cell in Cells)
            {
                cell.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f);
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
                Debug.Log("Locked");
                //Snap animation
                foreach (Cell cell in Cells)
                {
                    cell.transform.DOScale(new Vector3(1f, 1f, 1f), 0f);
                }
            }
            else
            {
                //Restore animation
                transform.DOScale(new Vector3(1, 1, 1), 0.5f);
                foreach (Cell cell in Cells)
                {
                    cell.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
                }
                transform.DOMove(new Vector3(startPosX, startPosY, 0), 0.5f);
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
