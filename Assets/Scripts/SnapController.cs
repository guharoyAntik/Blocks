using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public static SnapController Instance;

    public List<BoardCell> snapPoints;

    public float SnapRange;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool OnDragEnded(Block block)
    {
        BoardCell[] boardCells = new BoardCell[block.Cells.Length];

        for (int i = 0; i < block.Cells.Length; ++i)
        {
            BoardCell overlappedBoardCell = GetClosestSnapPointTransform(block.Cells[i]);
            if (overlappedBoardCell != null)
            {
                boardCells[i] = overlappedBoardCell;
            }
            else
            {
                return false;
            }
        }

        for (int i = 0; i < block.Cells.Length; ++i)
        {
            block.Cells[i].transform.position = boardCells[i].transform.position;
            boardCells[i].IsEmpty = false;
        }

        return true;
    }

    //Finds and returns transform of overlapped empty BoardCell if present
    public BoardCell GetClosestSnapPointTransform(Cell cell)
    {
        float closestDistance = float.MaxValue;
        BoardCell closestSnapPoint = null;

        foreach (BoardCell boardCell in snapPoints)
        {
            if (boardCell.IsEmpty)
            {
                float distance = Vector2.Distance(boardCell.transform.position, cell.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSnapPoint = boardCell;
                }
            }
        }

        return (closestDistance <= SnapRange) ? closestSnapPoint : null;
    }
}
