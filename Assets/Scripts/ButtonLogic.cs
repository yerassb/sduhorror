using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogic : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void clickStart() {
        Debug.Log("start is clilcked");
        SceneManager.LoadScene("SampleScene");
    }
    public void clickQuit() {
        Application.Quit();
    }
}
