using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Slider HealthBar;
    float horizontalMove;
    float verticalMove;
    private float speed = 5f;
    private Rigidbody rb;
    public float health = 100;
    private Animator animator;
    [SerializeField] private Camera cam;
    private Vector3 camdistance;
    [SerializeField] private GameObject myweapon;
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
            animator.SetInteger("Action", 1);
        }
        if(verticalMove !=0 || horizontalMove != 0)
        {
            animator.SetInteger("Action", 2);
            transform.rotation = Quaternion.LookRotation(new Vector3(horizontalMove, 0 , verticalMove), Vector3.up);
            if(verticalMove* horizontalMove != 0)
            {
                rb.velocity = new(horizontalMove * speed*0.7f, 0, verticalMove * speed * 0.7f);
            }
            else
            {
                rb.velocity = new(horizontalMove * speed , 0, verticalMove * speed);
            }
        }
        
    }

    private void Attack()
    {
        Debug.Log("attack!");
        animator.SetInteger("Action", 3);
        horizontalMove = 0;
        verticalMove = 0;
        myweapon.GetComponent<Collider>().enabled = true;
    }
    

    void Update()
    {
        HealthBar.value = health;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            
        }

        cam.transform.position = camdistance + transform.position;
    }

}
