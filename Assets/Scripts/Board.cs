using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    public int BoardRows;
    public int BoardColumns;

    [SerializeField]
    private BoardCell[] boardCells;

    private BoardCell[,] boardCellsGrid;

    private void Awake()
    {
        Instance = this;
        boardCellsGrid = new BoardCell[BoardRows, BoardColumns];

        int boardCellIdx = 0;
        for (int i = 0; i < BoardRows; ++i)
        {
            for (int j = 0; j < BoardColumns; ++j)
            {
                boardCellsGrid[i, j] = boardCells[boardCellIdx++];
            }
        }
    }

    public void CheckAndClear()
    {
        List<Tuple<int, int>> toRemove = new List<Tuple<int, int>>();

        //Row check
        for (int i = 0; i < BoardRows; ++i)
        {
            int filledInRow = 0;
            for (int j = 0; j < BoardColumns; ++j)
            {
                if (boardCellsGrid[i, j].IsEmpty)
                {
                    break;
                }
                filledInRow++;
            }

            if (filledInRow == BoardColumns)
            {
                for (int j = 0; j < BoardColumns; ++j)
                {
                    toRemove.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        //Column check
        for (int j = 0; j < BoardColumns; ++j)
        {
            int filledInColumn = 0;
            for (int i = 0; i < BoardRows; ++i)
            {
                if (boardCellsGrid[i, j].IsEmpty)
                {
                    break;
                }
                filledInColumn++;
            }

            if (filledInColumn == BoardRows)
            {
                for (int i = 0; i < BoardRows; ++i)
                {
                    toRemove.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        foreach (Tuple<int, int> idx in toRemove)
        {
            boardCellsGrid[idx.Item1, idx.Item2].IsEmpty = true;
        }
    }

    //Finds and returns overlapped empty BoardCell if present
    public BoardCell GetOverlappedBoardCell(Cell cell)
    {
        float snapRange = SnapController.Instance.SnapRange;
        float closestDistance = float.MaxValue;
        BoardCell closestSnapPoint = null;

        foreach (BoardCell boardCell in boardCells)
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


        return (closestDistance <= snapRange) ? closestSnapPoint : null;
    }

    //Checks and returns true if a block can be placed on the board
    bool CanPlaceBlockOnBoard(Block block)
    {
        for (int i = 0; i < BoardRows; ++i)
        {
            for (int j = 0; j < BoardColumns; ++j)
            {
                if (boardCellsGrid[i, j].IsEmpty)
                {
                    bool canBePlaced = true;
                    float xOffset = boardCellsGrid[i, j].transform.position.x - block.Cells[0].transform.position.x;
                    float yOffset = boardCellsGrid[i, j].transform.position.y - block.Cells[0].transform.position.y;

                    for (int blockCellIdx = 1; blockCellIdx < block.Cells.Length; ++blockCellIdx)
                    {
                        BoardCell overlappedBoardCell = GetOverlappedBoardCell(block.Cells[blockCellIdx]);
                        if (overlappedBoardCell == null)
                        {
                            canBePlaced = false;
                            break;
                        }
                    }

                    if (canBePlaced == true)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
