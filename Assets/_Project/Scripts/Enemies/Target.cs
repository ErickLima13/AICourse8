//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public GameObject[] decalBlood;

    private Animator animator;

    private void Initialization()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Initialization();
    }

    public void TakeDamage(float amount)
    {
        animator.Play("Hit");

        health -= amount;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int n = Random.Range(1, decalBlood.Length);
        GameObject effectGO = Instantiate(decalBlood[n], transform.position, Quaternion.identity);
        effectGO.SetActive(true);
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
