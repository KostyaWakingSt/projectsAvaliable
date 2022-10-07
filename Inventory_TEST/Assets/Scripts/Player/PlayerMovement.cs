using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody2D rb;
    [Header("Control Settings")]
    public float speed;
    public float jumpForce;
    public KeyCode[] GetKeyCodesInv;

    [Header("UI")]
    public GameObject invPanel;
    [SerializeField] private Inventory inv;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnGUI()
    {
        int fps = (int)(1.0f / Time.deltaTime);
        GUILayout.Label("FPS = " + fps);
    }

    private void Update()
    {

        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal") * Time.deltaTime, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < inv.slots.Count; i++)
            {
                if (inv.slots[i].isApply)
                {
                    if (inv.slots[i].nameObject != null)
                    {
                        inv.Drop(inv.items[inv.slots[i].idObject].idObject);
                    }
                }
            }
        }
        OnButtonClickInv();
        invPanel.SetActive(Input.GetKey(KeyCode.Tab) ? true : false);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnButtonClickInv()
    {
        for(int i = 0; i < GetKeyCodesInv.Length; i++)
        {
            if (Input.GetKeyDown(GetKeyCodesInv[i]))
            {
                for(int b = 0; b < inv.slots.Count; b++)
                {
                    if(inv.slots[i] == inv.slots[b])
                    {
                        inv.slots[i].isApply = true;
                    } else
                    {
                        inv.slots[b].isApply = false;
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            inv.Equip(collision.gameObject.GetComponent<Item>().idObject);
            Destroy(collision.gameObject);
        }
    }
}
