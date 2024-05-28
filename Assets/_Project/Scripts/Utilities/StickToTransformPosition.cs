using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToTransformPosition : MonoBehaviour
{
    [SerializeField] Transform targetToStickTo;

    void Update()
    {
        if (!targetToStickTo) return;

        transform.position = targetToStickTo.position;

        //transform.eulerAngles = new Vector3(0f,0f,0f);
    }
}
