using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Destroy : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Light")
        {
            Destroy(other.transform.gameObject);
        }
    }
}
