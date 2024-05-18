using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] gameOver gg;
    [SerializeField] LayerMask layerMask;

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

        Vector2 playerDir = (thePlayer.transform.position - transform.position);
        playerDir.y += -0.75f;
        playerDir = playerDir.normalized;

        if(playerDir.x < 0) {
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }
        RaycastHit2D ray;
        if(ray = Physics2D.Raycast(transform.position, playerDir, Vector3.Distance(thePlayer.transform.position, transform.position), layerMask)) {
            //(-y, x)
            if(ray.collider.gameObject.tag != "Player") {
                playerDir = new Vector2(-playerDir.y, playerDir.x);
            }
        }        
        rb.MovePosition(rb.position + playerDir * speed * Time.fixedDeltaTime);        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            gg.gameLost();
        }
    }    
}
