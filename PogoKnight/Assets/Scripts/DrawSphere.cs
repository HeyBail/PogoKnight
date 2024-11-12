using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphere : MonoBehaviour
{
    [SerializeField]
    private float size = .5f;

    [SerializeField]
    private Color color = Color.blue;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
