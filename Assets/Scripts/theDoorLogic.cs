using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class theDoorLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject collectiblesParent;
    public TextMeshProUGUI notification;
    int numberOfKeys;
    int totalNo;
    void Start()
    {   
        totalNo = collectiblesParent.transform.childCount;
        numberOfKeys = totalNo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void keyFound() {
        numberOfKeys--;
        notifyPlayer();
    }
    void notifyPlayer() {
        if(numberOfKeys > 0) {
            notification.text = "Key Found! \n " + (totalNo - numberOfKeys) + " / " + totalNo;
        } else {
            notification.text = "Now you can open the door!";
        }
        StartCoroutine(flashText());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.tag == "Player") {
            if(numberOfKeys == totalNo) {
                notification.text = "You need to find keys to unlock this door";
            } else if(numberOfKeys > 0) {
                notification.text = "Keep looking, you already found " + (totalNo - numberOfKeys);
            } else {
                Destroy(gameObject);
                notification.text = "Door is unlocked!";                
            }
            StartCoroutine(flashText());
        }
    } 
    IEnumerator flashText() {
        notification.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        notification.gameObject.SetActive(false);

    }
}
