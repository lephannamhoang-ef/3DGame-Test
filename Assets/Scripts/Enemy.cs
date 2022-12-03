using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    private Animator animator;
    [SerializeField] private Slider healthbar;
    private BoxCollider boxcollider;
    void Start()
    {
        animator = GetComponent<Animator>();
        healthbar.maxValue = health;
        boxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = health;
        if(healthbar.value == 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        animator.SetBool("isDead", true);
        boxcollider.size = new(1, 0.5f, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Sword")== true)
        {
            health -= 50;
        }
    }
}
