using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRunning;
    void Start()
    {
        isRunning = true;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameLost() {
        if(isRunning) {
            Debug.Log("YOU LOSE THE GAME");
            isRunning = false;
        }
    }
    public void gameWon() {
        if(isRunning) {
            Debug.Log("YOU WIN THE GAME");
            isRunning = false;
        }


    }
}
