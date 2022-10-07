using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    public static RandomGeneration Instance { get; set; }


    public float xOne = 0, xTwo = 0;
    public float yOne = 0, yTwo = 0;

    public int lengthGeneration;

    private void Start()
    {
        Instance = this;
        Invoke("Generation_Island", 0.5f);
    }


    public void Generation_Flat()
    {
        for (int i = 0; i < lengthGeneration; i++)
        {
            var block = Instantiate(MinerLogic.Instance.blocks[0].gameObject, new Vector2(xOne, yOne), Quaternion.identity);
            MinerLogic.Instance.createdBlocks?.Add(block.GetComponent<BlockVariables>());
            xOne += 2;
        }
        for (int i = 0; i < lengthGeneration; i++)
        {
            var block = Instantiate(MinerLogic.Instance.blocks[0].gameObject, new Vector2(xTwo, yTwo), Quaternion.identity);
            MinerLogic.Instance.createdBlocks?.Add(block.GetComponent<BlockVariables>());
            xTwo -= 2;
        }
    }

    public void Generation_Island ()
    {
        for (int i = 0; i < lengthGeneration; i++)
        {
            var randY = Random.Range(-10, 10);
            if(randY % 2 != 0)
            {
                randY++;
            }
            var block = Instantiate(MinerLogic.Instance.blocks[0].gameObject, new Vector2(xOne, yOne), Quaternion.identity);
            MinerLogic.Instance.createdBlocks?.Add(block.GetComponent<BlockVariables>());
            xOne += 2;
        }
    }
}
