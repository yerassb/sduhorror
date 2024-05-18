using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleKeys : MonoBehaviour
{
    [SerializeField] public theDoorLogic door;
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Collision " + collision);
        if(collision.gameObject.tag == "Player") {
            Destroy(transform.gameObject);
            door.keyFound();
        }
    }
}
