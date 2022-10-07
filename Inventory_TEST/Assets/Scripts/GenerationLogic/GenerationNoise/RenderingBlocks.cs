using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderingBlocks : MonoBehaviour
{
    private void OnBecameVisible()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("visible");
    }

    private void OnBecameInvisible()
    {
        gameObject.layer = LayerMask.NameToLayer("blocks");
        Debug.Log("invisible");
    }
}
