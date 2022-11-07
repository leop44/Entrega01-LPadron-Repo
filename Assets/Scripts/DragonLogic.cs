using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DragonLogic : MonoBehaviour
{
    public int rut;
    public float timer;
    public Quaternion angle;
    public float grade;
    public Animator ani;
    public GameObject target;
    public bool attack;
    public float hpDragon = 100f;
    public GameObject die;
    public float destroyTime = 15;

    private void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }
    private void Conduct()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 20)
        {
            ani.SetBool("run", false);
            timer += 1 * Time.deltaTime;
            if (timer >= 4)
            {
                rut = Random.Range(0, 2);
                timer = 0;
            }
            switch (rut)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    grade = Random.Range(0, 360);
                    angle = Quaternion.Euler(0, grade, 0);
                    rut++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 5 && !attack)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                ani.SetBool("walk", false);
                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 4 * Time.deltaTime);
                ani.SetBool("attack", false);
            }
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);
                ani.SetBool("attack", true);
                attack = true;
            }
        }
    }
    private void Update()
    {
        Conduct();

        if (hpDragon <= 0f)
        {
            ani.SetBool("die", true);
            Destroy(die, destroyTime);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            ani.SetBool("getHit", true);
            hpDragon = hpDragon - 50f;
        }
    }
    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        attack = false;
    }
    public void Final_aniHit()
    {
        ani.SetBool("getHit", false);
    }
}
