using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movement : MonoBehaviour
{
    BoxCollider2D boxCollider;

    public GameObject Camera;

    float mvSpeed = 4f;
    float runSpeed = 7f;

    Rigidbody2D rb;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    Vector2 mv;
    private void Update() {
        float ad = Input.GetAxisRaw("Horizontal");
        float ws = Input.GetAxisRaw("Vertical");

        if(ad > 0) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        } else if (ad < 0) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

        }

        mv = new Vector2(ad, ws);
    }
    void FixedUpdate()
    {
        

        float speed = mvSpeed;

        if(Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
        }


        // Apply movement
        mv = Vector3.Normalize(mv);
        rb.MovePosition(rb.position + mv * speed * Time.fixedDeltaTime);

        //Vector3 newpos = new Vector3((float)ws, (float)ad) * speed;       

        //Collider2D[] hits = Physics2D.OverlapBoxAll(newpos, boxCollider.size, 0);

        //foreach(Collider2D hit in hits) {
        //    Debug.Log(hit);
        //    Debug.Log(hit == boxCollider);

        //    if(hit == boxCollider)
        //        transform.Translate(newpos);            
        //}

        cameraFollow();
    }
    private void OnCollisionEnter2D(Collision2D collision) {      
        rb.velocity = Vector2.zero;        
    }
    void cameraFollow() {
        float maxDelta = 20f;

        float newx = Mathf.MoveTowards(Camera.transform.position.x, transform.position.x, maxDelta);
        float newy = Mathf.MoveTowards(Camera.transform.position.y, transform.position.y, maxDelta);
        Vector3 newpos = new Vector3(newx, newy, Camera.transform.position.z);

        Camera.transform.position = newpos;
    }
}
