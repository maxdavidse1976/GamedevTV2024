using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAfterTime : MonoBehaviour
{
    [SerializeField] bool dontDestroy;
    public float LifeTime = 0.5f;

    float lifeTime;
    
    private void OnEnable()
    {
        lifeTime = LifeTime;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            if (dontDestroy)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}
