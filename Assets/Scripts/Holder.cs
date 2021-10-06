using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

public class Holder : MonoBehaviour
{

    public static Holder Instance;

    private Block[] _blocks;        //block gameobjects after instatiating

    private Vector3[] _blockPositions;   //fixed positions on board holder    

    private void Awake()
    {
        Instance = this;

        float y = transform.position.y;
        float width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        int numberOfBlocks = 4;

        _blocks = new Block[numberOfBlocks];
        _blockPositions = new Vector3[numberOfBlocks];
        _blockPositions[0] = new Vector3(-0.6f * width, 0, 0) + transform.position;
        _blockPositions[1] = new Vector3(0.0f * width, 0, 0) + transform.position;
        _blockPositions[2] = new Vector3(0.6f * width, 0, 0) + transform.position;
        _blockPositions[3] = new Vector3(1.5f * width, 0, 0) + transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < _blockPositions.Length; ++i)
        {
            SpawnBlock(i);
        }
    }

    //Update block holder to check if a space has opened up then fill the space with next block
    public async Task UpdateHolder()
    {
        Sequence updateSeq = DOTween.Sequence();
        float slideInAnimationTime = 0.333f;

        for (int i = 0; i < _blockPositions.Length - 1; ++i)
        {
            if (_blocks[i] != null)
            {
                _blocks[i].DisableBlock();  // Stop interaction with blocks during animation
            }
            if (_blocks[i] == null || _blocks[i].transform.position != _blockPositions[i])
            {
                _blocks[i] = _blocks[i + 1];
                _blocks[i + 1] = null;
                updateSeq.Insert(0f, _blocks[i].transform.DOMove(_blockPositions[i], slideInAnimationTime));
                _blocks[i].PositionInHolder = _blockPositions[i];
            }
        }

        await updateSeq.Play().AsyncWaitForCompletion();

        if (_blocks[_blocks.Length - 1] == null)
        {
            SpawnBlock(_blocks.Length - 1);
        }

        int validBlocksCount = 0;
        for (int i = 0; i < _blocks.Length - 1; ++i)
        {
            // Check if block can be placed
            bool canPlace = Board.Instance.CanPlaceBlockOnBoard(_blocks[i]);

            // Enable all valid blocks
            if (canPlace == true)
            {
                _blocks[i].EnableBlock();
                validBlocksCount++;
            }
            else
            {
                _blocks[i].DisableBlock();
            }
        }

        if (validBlocksCount == 0)      // No more moves
        {
            GameManager.Instance.GameOver();
        }
    }

    public void SpawnBlock(int idx)
    {
        GameObject spawnedBlock = SpawnManager.Instance.SpawnBlock();
        spawnedBlock.transform.localScale = new Vector3(1, 1, 1);
        spawnedBlock.transform.position = _blockPositions[idx];
        _blocks[idx] = spawnedBlock.GetComponent<Block>();
        _blocks[idx].PositionInHolder = _blockPositions[idx];
    }
}
