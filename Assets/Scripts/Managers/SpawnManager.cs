using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private GameObject[] _blockPrefabs;  //prefabs to instantiate from

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public GameObject SpawnBlock()
    {
        int spawnIdx = Random.Range(0, _blockPrefabs.Length);
        GameObject spawnedBlock = Instantiate(
            _blockPrefabs[spawnIdx],
            _blockPrefabs[spawnIdx].transform.position,
            _blockPrefabs[spawnIdx].transform.rotation,
            Holder.Instance.transform);

        int fillType = Random.Range(0, CellSprites.Instance.FillVariants.Length);
        spawnedBlock.GetComponent<Block>().FillBlock(fillType);

        return spawnedBlock;
    }
}
