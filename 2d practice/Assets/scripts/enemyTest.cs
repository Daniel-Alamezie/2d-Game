using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
    [SerializeField]
    Transform leftRaycast;
    [SerializeField]
    Transform rightRaycast;

    [SerializeField]
    Transform player;

    bool isHitEnemy = false;
    int pos;

    public float rayLength= 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 endPosLeft = leftRaycast.position + Vector3.left * rayLength;
        Vector2 endposRight = rightRaycast.position + Vector3.right * rayLength;
        RaycastHit2D left = Physics2D.Linecast(leftRaycast.position,endPosLeft, 1 << LayerMask.NameToLayer("Ground"));
        RaycastHit2D right = Physics2D.Linecast(rightRaycast.position, endposRight, 1 << LayerMask.NameToLayer("Ground"));

        Debug.DrawLine(leftRaycast.position, endPosLeft, Color.white);
        Debug.DrawLine(rightRaycast.position, endposRight, Color.white);
        try
        {

            if (left.collider != null)
            {
                if (left.collider.gameObject.CompareTag("Player"))
                {
                    //set position to 1 if facing left of player
                    //Then chase player towards that direction 
                    pos = 1;
                    chasPlayer();

                }
                else
                {
                    stopChase();
                }
            }
            else if (right.collider != null)
            {
                if (right.collider.gameObject.CompareTag("Player"))
                {
                    //set position to -1 if facing right of player
                    //Then chase player towards that direction 
                    pos = -1;
                    chasPlayer();

                }

            }
        }
        catch { }
        
       

    }


    private void stopChase()
    {
        throw new NotImplementedException();
    }

    private void chasPlayer()
    {
        if (pos == -1)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(8f, player.position.x),10f*Time.fixedDeltaTime );

        }
        else if (pos == 1)
        {
           transform.position = Vector2.MoveTowards(transform.position, new Vector2(-8f, player.position.x), 10f*Time.fixedDeltaTime);

        }
    }


}
