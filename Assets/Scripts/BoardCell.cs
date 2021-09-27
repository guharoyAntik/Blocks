using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    private static Color EmptyCellColor = new Color(0.5294118f, 0.5294118f, 0.5294118f, 1f);
    private static Color FilledCellColor = new Color(1f, 1f, 1f, 1f);
    private bool isEmpty;

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
            return isEmpty;
        }
        set
        {
            isEmpty = value;
            spriteRenderer.color = (value == true ? EmptyCellColor : FilledCellColor);
        }
    }
}
