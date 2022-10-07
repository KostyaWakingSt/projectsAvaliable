using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerLogic : MonoBehaviour
{
    public static MinerLogic Instance { get; set; }

    public Vector3 mousePos;
    public Vector3 mousePosInt;
    public List<BlockVariables> blocks;
    public List<BlockVariables>? createdBlocks;
    private List<Slot> slots;
    private bool isAllowCreateBlock = true;

    public Transform playerRay;
    public byte blockSizeMod;
    private GameObject fantomBlock;

    public void Start()
    {
        Instance = this;
        fantomBlock = Instantiate(MinerLogic.Instance.blocks[2].gameObject);
    }

    private void Update()
    {
        //create block
        slots = Inventory.Instance.slots;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        fantomBlock.transform.position = new Vector2(Mathf.Round(mousePos.x / blockSizeMod) * blockSizeMod, Mathf.Round(mousePos.y / blockSizeMod) * blockSizeMod);

        foreach (var item in createdBlocks)
        {
            if (fantomBlock.transform.position == item.transform.position)
            {
                isAllowCreateBlock = false;
                fantomBlock.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            }
            else
            {
                isAllowCreateBlock = true;
                fantomBlock.GetComponent<SpriteRenderer>().color = Color.green;
                continue;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isAllowCreateBlock)
            {
                if (createdBlocks.Count > 0)
                {

                    for (int i = 0; i < slots.Count; i++)
                    {
                        if (slots[i].isApply)
                        {
                            if (slots[i].isBuild)
                            {
                                var block = Instantiate(blocks[slots[i].idBlock].gameObject, new Vector2(Mathf.Round(mousePos.x / blockSizeMod) * blockSizeMod, Mathf.Round(mousePos.y / blockSizeMod) * blockSizeMod), Quaternion.identity);
                                createdBlocks.Add(block.GetComponent<BlockVariables>());
                            }
                            else
                            {
                                break;
                            }
                        }
                        else { continue; }
                    }
                }
                else
                {
                    for (int i = 0; i < slots.Count; i++)
                    {
                        if (slots[i].isApply)
                        {
                            if (slots[i].isBuild)
                            {
                                var block = Instantiate(blocks[slots[i].idBlock].gameObject, new Vector2(Mathf.Round(mousePos.x / blockSizeMod) * blockSizeMod, Mathf.Round(mousePos.y / blockSizeMod) * blockSizeMod), Quaternion.identity);
                                createdBlocks.Add(block.GetComponent<BlockVariables>());
                            }
                            else
                            {
                                break;
                            }
                        }
                        else { continue; }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!isAllowCreateBlock)
            {
                for(int i = 0; i < createdBlocks.Count; i++)
                {
                    if(fantomBlock.transform.position == createdBlocks[i].transform.position)
                    {
                        Destroy(createdBlocks[i].gameObject);
                        createdBlocks.RemoveAt(i);
                    }
                }
            }
        }

    }
}
