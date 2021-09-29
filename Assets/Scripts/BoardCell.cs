using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    private static Color _emptyCellColor = new Color(0.5294118f, 0.5294118f, 0.5294118f, 1f);
    private static Color _filledCellColor = new Color(1f, 1f, 1f, 1f);
    private bool _isEmpty;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        IsEmpty = true;
    }

    public bool IsEmpty
    {
        get
        {
            return _isEmpty;
        }
        set
        {
            _isEmpty = value;
            spriteRenderer.color = (value == true ? _emptyCellColor : _filledCellColor);
        }
    }
}
