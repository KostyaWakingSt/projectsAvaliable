using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 2f)] public float DampTime = 0.5f;
    private Vector3 Velocity = Vector3.zero;
    public Transform Target;


    void Update()
    {
        if (Target)
        {
            Vector3 Point = Camera.main.WorldToViewportPoint(new Vector3(Target.position.x, Target.position.y, Target.position.z));
            Vector3 Delta = new Vector3(Target.position.x, Target.position.y, Target.position.z) - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 Destination = transform.position + Delta;
            transform.position = Vector3.SmoothDamp(transform.position, Destination, ref Velocity, DampTime);

        }
    }
}