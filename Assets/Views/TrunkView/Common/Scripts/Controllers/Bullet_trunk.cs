using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_trunk : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bullet_speed;
    private float timer;
    public bool facingLeft = true;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (facingLeft) 
         {
            rb.velocity = Vector2.left * bullet_speed;
        }
        else
        {
            rb.velocity = Vector2.right * bullet_speed;
        }

        if (timer > 2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLifeController>().LoseHealth();
            Destroy(gameObject);
        }
    }
}
