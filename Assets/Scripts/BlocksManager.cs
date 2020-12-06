using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    [SerializeField] GameObject[] blocks = new GameObject[0];
    [SerializeField] Transform player = null;
    [SerializeField] float safeZone = 35;

    int onScreenBlocks = 5;
    float zPoint = -25;
    float blockLength = 25;
    int lastBlockIndex;

    List<GameObject> SpawnedBlocks = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < onScreenBlocks; i++)
        {
            if(i < 2)
            {
                SpawnBlock(0);
            }
            else
            {
                SpawnBlock();
            }
        }
    }

    private void Update()
    {
        if (player.transform.position.z - safeZone > (zPoint - onScreenBlocks * blockLength))
        {
            SpawnBlock();
            DeleteBlock();
        }
    }

    private void SpawnBlock(int blockIndex = -1)
    {
        GameObject blockInstance;

        if (blockIndex == -1)
        {
            blockInstance = Instantiate(blocks[RandomBlockIndex()]) as GameObject;
        }
        else
        {
            blockInstance = Instantiate(blocks[blockIndex]) as GameObject;
        }

        blockInstance.transform.SetParent(transform);
        blockInstance.transform.position = Vector3.forward * zPoint;
        zPoint += blockLength;
        SpawnedBlocks.Add(blockInstance);
    }

    private void DeleteBlock()
    {
        Destroy(SpawnedBlocks[0]);
        SpawnedBlocks.RemoveAt(0);
    }

    private int RandomBlockIndex()
    {
        int randomBlockIndex = lastBlockIndex;
        if (blocks.Length <= 1)
        {
            return 0;
        }

        while (randomBlockIndex == lastBlockIndex)
        {
            randomBlockIndex = Random.Range(0, blocks.Length);
        }

        lastBlockIndex = randomBlockIndex;
        return randomBlockIndex;

    }

}
