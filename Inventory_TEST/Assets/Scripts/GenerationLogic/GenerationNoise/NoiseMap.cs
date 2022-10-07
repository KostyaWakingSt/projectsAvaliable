using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseMap : MonoBehaviour
{
    [Header("Settings")]
    public Vector2Int size;
    public float zoom;
    public Vector2 offset;
    public float intensivity;
    public float cutPlane;
    public Transform blocks;
    private bool isAllowCreateBlock;

    public void genTileCave()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                var p = Mathf.PerlinNoise((x + offset.x) / zoom, (y + offset.y) / zoom) * intensivity;
                var gr = p;

                if (gr > cutPlane)
                {
                    foreach (var item in MinerLogic.Instance.createdBlocks)
                    {
                        if (item.gameObject.transform.position != new Vector3(Mathf.Round(x / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, Mathf.Round(y / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, 0))
                        {
                            isAllowCreateBlock = true;
                            continue;
                        }
                        else
                        {
                            isAllowCreateBlock = false;
                            break;
                        }
                    }
                    if (isAllowCreateBlock)
                    {
                        if (gr < 1f && gr > 0.8f)
                        {
                            var block = Instantiate(MinerLogic.Instance.blocks[0].gameObject, new Vector3(Mathf.Round(x / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, Mathf.Round(y / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, 0), Quaternion.identity);
                            MinerLogic.Instance.createdBlocks?.Add(block.GetComponent<BlockVariables>());
                            block.transform.SetParent(blocks);
                        }
                        else if (gr < 0.8F)
                        {
                            var block = Instantiate(MinerLogic.Instance.blocks[0].gameObject, new Vector3(Mathf.Round(x / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, Mathf.Round(y / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, 0), Quaternion.identity);
                            MinerLogic.Instance.createdBlocks?.Add(block.GetComponent<BlockVariables>());
                            block.transform.SetParent(blocks);
                        }
                        else
                        {
                            var block = Instantiate(MinerLogic.Instance.blocks[1].gameObject, new Vector3(Mathf.Round(x / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, Mathf.Round(y / MinerLogic.Instance.blockSizeMod) * MinerLogic.Instance.blockSizeMod, 0), Quaternion.identity);
                            MinerLogic.Instance.createdBlocks?.Add(block.GetComponent<BlockVariables>());
                            block.transform.SetParent(blocks);
                        }
                    }

                }
            }
        }
    }

    void Start()
    {
        intensivity = Random.Range(0.80f, 1.32f);
        cutPlane = Random.Range(0.38f, 0.56f);
        offset.x = Random.Range(0, 10000);
        offset.y = Random.Range(0, 10000);
        Invoke("genTileCave", 0.5f);
    }
}
