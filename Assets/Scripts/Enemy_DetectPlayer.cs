using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DetectPlayer : MonoBehaviour
{
    public Animator ani;
    public GruntLogic enemy;
    public DragonLogic dragon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ani.SetBool("walk", false);
            ani.SetBool("run", false);
            ani.SetBool("attack", true);
            enemy.attack = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
