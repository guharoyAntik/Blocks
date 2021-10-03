using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public int FillType;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanBePlaced()
    {
        BoardCell overlappedBoardCell = SnapController.Instance.GetClosestSnapPoint(this);

        return overlappedBoardCell != null;
    }

    public void SetFillType(int newFillType)
    {
        FillType = newFillType;
        _spriteRenderer.sprite = CellSprites.Instance.FillVariants[FillType];
    }
}
