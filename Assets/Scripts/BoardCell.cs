using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    private bool _isEmpty = true;

    [HideInInspector]
    public int FillType;

    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Animator _animator;

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
        if (_isEmpty == false)
        {
            if (FillType == 0)
            {
                _animator.Play("cell-blue-remove");
            }
            else if (FillType == 1)
            {
                _animator.Play("cell-red-remove");
            }
        }
        _isEmpty = true;
        _spriteRenderer.sprite = null;
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
