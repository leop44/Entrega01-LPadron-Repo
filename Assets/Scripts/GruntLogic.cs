using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GruntLogic : MonoBehaviour
{
    public int rut;
    public float timer;
    public Quaternion angle;
    public float grade;
    public Animator ani;
    public GameObject target;
    public bool attack;
    public Enemy_DetectPlayer range;
    public float hpGrunt = 20f;
    public GameObject die;
    public float destroyTime = 1;
    public Vector3 lookPos;
    public Quaternion rotation;
    public GameObject coin;
    public bool coinDrop;
    public GameObject coinLight;
    public bool coinLightSpawn;
    public GameObject detectSound;
    public bool growlSound;

    private void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.Find("Player");
        coinDrop = true;
        coinLightSpawn = true;
        growlSound = true;
    }
    private void Conduct()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 10)
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
            if (growlSound == true)
            {
                Instantiate(detectSound);
                growlSound = false;
            }
            lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            rotation = Quaternion.LookRotation(lookPos);
            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !attack)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                ani.SetBool("walk", false);
                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 4 * Time.deltaTime);
                ani.SetBool("attack", false);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

            }
        }
    }
    private void Update()
    {
        Conduct();

        if (hpGrunt <= 0f && coinDrop == true)
        {
            ani.SetBool("die", true);
            Destroy(die, destroyTime);
            Instantiate(coin, transform.position, transform.rotation);
            coinDrop = false;
            Instantiate(coinLight, transform.position, transform.rotation);
            coinLightSpawn = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            ani.SetBool("getHit", true);
            hpGrunt = hpGrunt - 7f;
        }
    }
    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        attack = false;
        range.GetComponent<CapsuleCollider>().enabled = true;
    }
    public void Final_aniHit() 
    {
        ani.SetBool("getHit", false);
    }
}
