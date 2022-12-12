using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class HealthSystem : MonoBehaviour
{
    bool hasHitDeathLine = false;
    bool hasHitHealth = false;
    bool hasHitEnemy =false;
    public float health;
    public float maxHealth = 100f;
    public static Vector2 currentCheckpoint;

    List<Vector2> points;
    [SerializeField]
    private healthBar HB;
    [SerializeField]

    private void Awake()
    {
        points[0] = transform.position;
    }


    private void Start()
    {
       
        health = maxHealth;
        HB.setMaxHealth(maxHealth);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //set true everytime player contacts world harm
        if (collision.gameObject.CompareTag("harm"))
        {
            Debug.Log("HIT");
            hasHitEnemy = true;
        }
        
        //set true everytime player contacts healthpack
        if (collision.gameObject.CompareTag("Health"))
        {
            Debug.Log("Healing");
            hasHitHealth = true;

        }
        //kill player on contact with deathline 
        if (collision.gameObject.CompareTag("DeathLine"))
        {
            Debug.Log("DeathLine");
            Destroy(gameObject);
           
            Respawn();
        }

        if(hasHitHealth == true && health!=100)
        {
            heal(8.5f);
            Debug.Log(health);
        }
        //update checkpoint spawn on collision with new checkpoint
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpoint = transform.position;
            points.Add(currentCheckpoint);
            Debug.Log(points);


            Debug.Log("RESPAWN!");
        }

       
        hasHitHealth = false;
        hasHitEnemy = false;

    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            takeDamage(6.2f);
            Debug.Log(health);
        }
        if (collision.gameObject.CompareTag("harm"))
        {
            takeDamage(15.10f);
        }
    }



    //handles damage
    void takeDamage(float amount)
    {
        health -= amount;

        //health check
        if (health <= 0)
        {
            health = 0;
        }

        if(health == 0)
        {
            Destroy(gameObject);
            Respawn();
        }
        // play death animation

        HB.setHealth(health);
    }
    

    //handles healing
    void heal(float amount) 
    {
        health += amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        HB.setHealth(health);
    }

    //handles respawn
    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    
}
