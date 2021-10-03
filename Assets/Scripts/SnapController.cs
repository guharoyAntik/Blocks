using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class SnapController : MonoBehaviour
{
    public static SnapController Instance;

    public float SnapRange;

    private float _animationTime = 0.2f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public async Task OnDragEnded(Block block)
    {
        BoardCell[] boardCells = new BoardCell[block.Cells.Length];

        for (int i = 0; i < block.Cells.Length; ++i)
        {
            BoardCell overlappedBoardCell = GetClosestSnapPoint(block.Cells[i]);
            if (overlappedBoardCell != null)
            {
                boardCells[i] = overlappedBoardCell;
            }
            else    //Cannot Snap
            {
                // Return to holder animation
                block.DisableBlock();   // Stop interaction with block during animation
                Sequence returnSeq = DOTween.Sequence();

                returnSeq.Insert(0, block.transform.DOScale(block.BlockInitialScale, _animationTime * 2));
                returnSeq.Insert(0, block.transform.DOMove(block.PositionInHolder, _animationTime * 2));
                foreach (Cell cell in block.Cells)
                {
                    returnSeq.Insert(0, cell.transform.DOScale(block.CellInitialScale, _animationTime * 2));
                }
                await returnSeq.Play().AsyncWaitForCompletion();
                block.EnableBlock();    // Restore interactibility of block

                return;
            }
        }

        //Can Snap        
        Sequence snapSeq = DOTween.Sequence();
        for (int i = 0; i < block.Cells.Length; ++i)
        {
            block.Cells[i].transform.position = boardCells[i].transform.position;

            snapSeq.Insert(0, block.Cells[i].transform.DOScale(block.CellInitialScale, _animationTime));
        }
        await snapSeq.Play().AsyncWaitForCompletion();

        //Fill Each board Cell
        for (int i = 0; i < boardCells.Length; ++i)
        {
            boardCells[i].FillCell(block.Cells[0].FillType);
        }

        //Fill gap created in Blocks Holder
        _ = Holder.Instance.UpdateHolder();

        //destroy block        
        return;
    }

    //Finds and returns transform of overlapped empty BoardCell if present
    public BoardCell GetClosestSnapPoint(Cell cell)
    {
        return Board.Instance.GetOverlappedBoardCell(cell);
    }
}
