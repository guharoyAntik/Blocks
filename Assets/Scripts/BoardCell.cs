using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    //private static Color _emptyCellColor = new Color(0.5294118f, 0.5294118f, 0.5294118f, 1f);
    //private static Color _filledCellColor = new Color(1f, 1f, 1f, 1f);
    private bool _isEmpty;

    public int FillType;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ClearCell();
    }

    public void ClearCell()
    {
        _isEmpty = true;
        _spriteRenderer.sprite = CellSprites.Instance.Empty;
    }

    public void FillCell(int newFillType)
    {
        _isEmpty = false;
        FillType = newFillType;
        _spriteRenderer.sprite = CellSprites.Instance.FillVariants[FillType];
    }

    public bool IsEmpty
    {
        get
        {
            return _isEmpty;
        }
    }
}
