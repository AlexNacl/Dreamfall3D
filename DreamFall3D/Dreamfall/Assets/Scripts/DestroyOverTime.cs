using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float LifeTime;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, LifeTime);
    }
}
