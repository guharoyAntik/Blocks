using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    [SerializeField] private GameObject _cellPrefab;
    public int BoardRows;
    public int BoardColumns;

    [SerializeField]
    private BoardCell[] _boardCells;
    private BoardCell[,] _boardCellsGrid;

    private void Awake()
    {
        Instance = this;

        //Initializing _boardCellsGrid
        _boardCellsGrid = new BoardCell[BoardRows, BoardColumns];
        int boardCellIdx = 0;
        for (int i = 0; i < BoardRows; ++i)
        {
            for (int j = 0; j < BoardColumns; ++j)
            {
                _boardCellsGrid[i, j] = _boardCells[boardCellIdx++];
            }
        }
    }

    //Checks the board for any combinations and then removes if present
    public void CheckAndClear()
    {
        List<Tuple<int, int>> toRemove = new List<Tuple<int, int>>();

        //Row check
        for (int i = 0; i < BoardRows; ++i)
        {
            int filledInRow = 0;
            for (int j = 0; j < BoardColumns; ++j)
            {
                if (_boardCellsGrid[i, j].IsEmpty)
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
                if (_boardCellsGrid[i, j].IsEmpty)
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

        //Cells Removal
        foreach (Tuple<int, int> idx in toRemove)
        {
            _boardCellsGrid[idx.Item1, idx.Item2].IsEmpty = true;
        }
    }

    //Finds and returns overlapped empty BoardCell if present
    public BoardCell GetOverlappedBoardCell(Cell cell)
    {
        float snapRange = SnapController.Instance.SnapRange;
        float closestDistance = float.MaxValue;
        BoardCell closestSnapPoint = null;

        foreach (BoardCell boardCell in _boardCells)
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
    public bool CanPlaceBlockOnBoard(Block block)
    {
        Cell offsetCell = _cellPrefab.GetComponent<Cell>();

        for (int i = 0; i < BoardRows; ++i)
        {
            for (int j = 0; j < BoardColumns; ++j)
            {
                if (_boardCellsGrid[i, j].IsEmpty)
                {
                    bool canBePlaced = true;
                    Vector3 curBoardCellPos = _boardCellsGrid[i, j].transform.position;

                    //Checking if all cells of block can be placed
                    for (int blockCellIdx = 0; blockCellIdx < block.Cells.Length; ++blockCellIdx)
                    {
                        //Multiplying by scale difference between block in holder and block on board
                        float xOffset = (block.Cells[blockCellIdx].transform.position.x - block.Cells[0].transform.position.x) * 2;
                        float yOffset = (block.Cells[blockCellIdx].transform.position.y - block.Cells[0].transform.position.y) * 2;

                        offsetCell.transform.position = new Vector3(curBoardCellPos.x + xOffset, curBoardCellPos.y + yOffset, 0);
                        BoardCell overlappedBoardCell = GetOverlappedBoardCell(offsetCell);


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
