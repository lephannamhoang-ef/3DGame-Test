using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider HealthBar;
    private float horizontalMove;
    private float verticalMove;
    private float speed = 5f;
    private Rigidbody rb;
    public float health = 100;
    private Animator animator;
    [SerializeField] private Camera cam;
    private Vector3 camdistance;
    [SerializeField] private GameObject myweapon;

    //combo
    private bool interupted = false;
    private static int nextAttack = 0;
    private int lastAttackTime;
    private float comboDelay = 1.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HealthBar.maxValue = health;
        camdistance = cam.transform.position - transform.position;
        
    }


    private void FixedUpdate()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        if(verticalMove ==0 && horizontalMove == 0)
        {
            animator.SetBool("isWalking", false);
        }
        if (interupted == false)
        {
            if (verticalMove != 0 || horizontalMove != 0)
            {
                animator.SetBool("isWalking", true);
                transform.rotation = Quaternion.LookRotation(new Vector3(horizontalMove, 0, verticalMove), Vector3.up);
                if (verticalMove * horizontalMove != 0)
                {
                    rb.velocity = new(horizontalMove * speed * 0.7f, 0, verticalMove * speed * 0.7f);
                }
                else
                {
                    rb.velocity = new(horizontalMove * speed, 0, verticalMove * speed);
                }
            }
        }
    }

    private void Attack()
    {   
        interupted = true;
        myweapon.GetComponent<Collider>().enabled = true;
        if (nextAttack >= 3) nextAttack = 0;
        nextAttack++;
        if(nextAttack == 1)
        {
            animator.SetTrigger("Attack1");
        }
        if(nextAttack == 2)
        {
            animator.SetTrigger("Attack2");
        }
        if (nextAttack == 3)
        {
            animator.SetTrigger("Attack3");
        }
        Debug.Log(nextAttack);
    }
    

    void Update()
    {
        HealthBar.value = health;
        
        cam.transform.position = camdistance + transform.position;
        cam.transform.LookAt(transform.position);

        if (interupted == true)
        {
            if (AnimatorIsPlaying("Attack1")==false && AnimatorIsPlaying("Attack2") == false && AnimatorIsPlaying("Attack3") == false)
            {
                interupted = false;
                myweapon.GetComponent<Collider>().enabled = false;
            }
        }
        else
        {
            //can attack
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }
        
    }


    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
