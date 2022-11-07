using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float hor;
    private float ver;
    private Vector3 velocity;
    public float movSpeed;
    public float rotSpeed;
    private bool jump;
    public float jumpForce;
    private bool fall;
    public float fallForce;
    public Rigidbody rb;
    public Animator anim;
    public bool attacking;
    public float hpHero = 100f;
    public Vector3 startPos;


    void Start()
    {
        startPos = transform.position;
        Cursor.lockState = CursorLockMode.Locked; // Cursor Locked
        Cursor.visible = false;
        jump = false;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // Movement & Rotation
        anim.SetBool("Jump", false);
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        velocity = Vector3.zero;
        float rotation = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0.0f, rotation * Time.deltaTime * rotSpeed, 0.0f));

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Attack
        {
            anim.SetTrigger("Attacking");
            attacking = true;
        }
        anim.SetFloat("SpdX", hor);
        anim.SetFloat("SpdY", ver);

        if (!attacking)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Jump
            {
                jump = true;
                fall = false;
                anim.SetBool("Jump", true);
            }
        }
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            fall = true;
            anim.SetBool("StopJump", true);
        }
        if (hpHero <= 0)
        {
            anim.SetBool("Die", true);
        }
        if (transform.position.y < -20) //Respawn by fall
        {
            Respawn();
        }
    }
    private void FixedUpdate()
    {
        if (!attacking && !jump)
        {
            if (hor != 0 || ver != 0)
            {
                Vector3 direction = (transform.forward * ver + transform.right * hor).normalized;
                velocity = direction * movSpeed;
            }
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }
        if (jump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
        else if (fall)
        {
            rb.AddForce(Vector3.down * fallForce);
        }
    }
    void Respawn()
    {
        transform.position = startPos;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            anim.SetBool("GetHit", true);
            hpHero = hpHero - 10f;
        }
    }
    public void StopAttacking() 
    {
        attacking = false;
    }
    public void Final_aniHit()
    {
        anim.SetBool("GetHit", false);
    }
}
