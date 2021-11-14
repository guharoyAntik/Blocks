using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSprites : MonoBehaviour
{
    public static CellSprites Instance;

    public Sprite Empty;
    public Sprite White;
    public Sprite[] FillVariants;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
