using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Destroy : MonoBehaviour
{
    public float destroyTime = 2f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
