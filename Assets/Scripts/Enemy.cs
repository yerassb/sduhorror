using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public GameObject thePlayer;
    Rigidbody2D rb;
    private float speed = 7f;
    private SpriteRenderer sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 playerDir = (thePlayer.transform.position - transform.position).normalized;
        if(playerDir.x < 0) {
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }
        rb.MovePosition(rb.position + playerDir * speed * Time.fixedDeltaTime);        
    }
}
