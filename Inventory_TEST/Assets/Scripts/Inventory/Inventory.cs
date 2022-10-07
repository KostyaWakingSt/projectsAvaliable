using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; set; }
    public List<Slot> slots;
    public List<Item> items;
    [SerializeField] private PlayerMovement GetPlayerMovement;

    private void Start()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            slots[i].idSlot = (byte)i;
        }
        Instance = this;
    }

    public void Equip(int _idObject)
    {
        foreach(var item in slots)
        {
            if(!item.isFull)
            {
                item.isFull = !item.isFull;
                item.idObject = _idObject;
                item.spriteObject = items[_idObject].spriteItem;
                item.GetComponent<Image>().sprite = item.spriteObject;
                item.nameObject = items[_idObject].nameItem;
                item.GetComponent<Image>().color = Color.red;
                item.isBuild = items[_idObject].isBuild;
                item.idBlock = items[_idObject].idBlock;
                return;
            }
        }
    }

    public void Drop(int _idObject)
    {
        GameObject itemGO = Instantiate(items[_idObject].gameObject, new Vector2(GetPlayerMovement.transform.position.x + 5, GetPlayerMovement.transform.position.y), Quaternion.identity);
                
        foreach(var item in slots)
        {
            if(item.isApply)
            {
                item.isFull = !item.isFull;
                item.nameObject = null;
                item.idObject = 0;
                item.spriteObject = null;
                item.GetComponent<Image>().sprite = item.spriteObject;
                item.isBuild = false;
                item.idBlock = -1;

                item.GetComponent<Image>().color = Color.white;

                return;
            }
        }
    }
}
