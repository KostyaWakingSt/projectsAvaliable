using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public string nameObject = null;
    public byte idSlot;
    public int idObject;
    public Sprite spriteObject;
    public bool isFull;
    public bool isApply;
    public bool isBuild;
    public int idBlock;

    public void OnClickSlot()
    {
        for(int i = 0; i < Inventory.Instance.slots.Count; i++)
        {
            if(idSlot == Inventory.Instance.slots[i].idSlot)
            {
                isApply = true;
            }
            else
            {
                Inventory.Instance.slots[i].isApply = false;
            }
        }
    }
}
