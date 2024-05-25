using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 1f, 0f);

    void LateUpdate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
