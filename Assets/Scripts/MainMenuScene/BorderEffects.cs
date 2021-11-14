using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderEffects : MonoBehaviour
{
    [SerializeField] private Image[] _borderCells;
    [SerializeField] private Sprite[] _blueSprites;
    [SerializeField] private Sprite[] _redSprites;
    [SerializeField] private Sprite _emptySprite;

    private int[] _spriteIdx;
    private int[] _spriteColor;

    private int _blueIdx;

    private void Start()
    {
        _blueIdx = 0;

        _spriteIdx = new int[_borderCells.Length];
        for (int i = 0; i < _spriteIdx.Length; ++i)
        {
            // if ((i + 10) % 30 < 15)
            // {
            //     _spriteIdx[i] = _blueSprites.Length / 2;
            // }
            _spriteIdx[i] = i % _blueSprites.Length;
        }

        _spriteColor = new int[_borderCells.Length];
        for (int i = 0; i < _spriteColor.Length; ++i)
        {
            _spriteColor[i] = ((i % 20 < 10) ? 0 : 1);
        }

        for (int i = 0; i < _borderCells.Length; ++i)
        {
            _borderCells[i].sprite = _emptySprite;
        }
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.1f);
            UpdateSprites();
        }

        yield return null;
    }

    public void UpdateSprites()
    {
        for (int cellIdx = 0; cellIdx < _borderCells.Length; ++cellIdx)
        {
            // if ((_spriteColor[cellIdx] == 0 && _spriteIdx[cellIdx] >= _blueSprites.Length) || (_spriteColor[cellIdx] == 1 && _spriteIdx[cellIdx] >= _redSprites.Length))
            if (_spriteIdx[cellIdx] < 0)
            {
                _spriteIdx[cellIdx] = (_spriteColor[cellIdx] == 0 ? _blueSprites.Length - 1 : _redSprites.Length - 1);
                _spriteColor[cellIdx] = (_spriteColor[cellIdx] + 1) % 2;
            }
            _borderCells[cellIdx].sprite = (_spriteColor[cellIdx] == 0 ? _blueSprites[_spriteIdx[cellIdx]] : _redSprites[_spriteIdx[cellIdx]]);

            _spriteIdx[cellIdx]--;
        }
    }
}
