using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookAtCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] bool freezeX = false;
    [SerializeField] bool freezeY = false;
    [SerializeField] bool freezeZ = false;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        Vector3 targetDirection = mainCamera.transform.position - transform.position;
        Vector3 newDirection = targetDirection;

        // Apply freezing of axes
        if (freezeX) newDirection.x = transform.forward.x;
        if (freezeY) newDirection.y = transform.forward.y;
        if (freezeZ) newDirection.z = transform.forward.z;

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
