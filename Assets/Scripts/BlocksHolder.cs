using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class BlocksHolder : MonoBehaviour
{
    public static BlocksHolder Instance;

    [SerializeField]
    private GameObject[] blockPrefabs;  //prefabs to instantiate from

    private Block[] blocks;        //block gameobjects after instatiating

    private Vector3[] blockPositions;   //fixed positions on board holder

    // private bool[] isBlockPositionFilled;   //check if position is filled

    private void Awake()
    {
        Instance = this;

        float y = transform.position.y;
        float width = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        int numberOfBlocks = 4;

        // isBlockPositionFilled = new bool[numberOfBlocks];
        blocks = new Block[numberOfBlocks];
        blockPositions = new Vector3[numberOfBlocks];
        blockPositions[0] = new Vector3(-0.6f * width, 0, 0) + transform.position;
        blockPositions[1] = new Vector3(0.0f * width, 0, 0) + transform.position;
        blockPositions[2] = new Vector3(0.6f * width, 0, 0) + transform.position;
        blockPositions[3] = new Vector3(1.5f * width, 0, 0) + transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < blockPositions.Length; ++i)
        {
            SpawnBlock(i);
        }
    }

    //Update block holder to check if a space has opened up then fill the space with next block
    public async Task UpdateHolder()
    {
        Sequence updateSeq = DOTween.Sequence();

        for (int i = 0; i < blockPositions.Length - 1; ++i)
        {
            if (blocks[i] == null || blocks[i].transform.position != blockPositions[i])
            {
                blocks[i] = blocks[i + 1];
                blocks[i + 1] = null;
                updateSeq.Insert(0f, blocks[i].transform.DOMove(blockPositions[i], 1f));
                blocks[i].PositionInHolder = blockPositions[i];
            }
        }

        await updateSeq.Play().AsyncWaitForCompletion();

        if (blocks[blocks.Length - 1] == null)
        {
            SpawnBlock(blocks.Length - 1);
        }

        for (int i = 0; i < blocks.Length - 1; ++i)
        {
            //TODO
        }
    }

    public void SpawnBlock(int idx)
    {
        int spawnIdx = Random.Range(0, blockPrefabs.Length);
        GameObject spawnedBlock = Instantiate(blockPrefabs[spawnIdx], blockPositions[idx], blockPrefabs[spawnIdx].transform.rotation, gameObject.transform);
        spawnedBlock.transform.localScale = new Vector3(1, 1, 1);
        spawnedBlock.transform.position = blockPositions[idx];
        blocks[idx] = spawnedBlock.GetComponent<Block>();
        blocks[idx].PositionInHolder = blockPositions[idx];
        // isBlockPositionFilled[idx] = true;
    }
}
