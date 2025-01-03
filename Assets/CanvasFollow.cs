using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    [SerializeField] Transform follow;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - follow.position;
    }

    void Update()
    {
        transform.position = follow.position + offset;
    }
}
