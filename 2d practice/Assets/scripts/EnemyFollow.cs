using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;
    Rigidbody2D rb2d;
    
    void Start()
    {
        rb2d.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        //check player distance
        float disToPlayer = Vector2.Distance(transform.position, player.position);
        if( disToPlayer< agroRange)
        {
            //start chase
            chasePlayer();

        }
        else 
        {
            //stop chase
            stopChase();
        }
    }

    private void stopChase()
    {
        
    }

    private void chasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
        }
        else if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
        }
    }
}
