using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnParentOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        transform.SetParent(null);
    }
}
