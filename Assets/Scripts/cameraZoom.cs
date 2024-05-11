using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    public GameObject Player;
    public float camMinSize;
    public float camMaxSize;

    Vector3 oldPos;
    Vector3 newPos;
    Vector3 v;

    Camera cameraComponent;
    

    void Start()
    {
        oldPos = Player.transform.position;
        newPos = Player.transform.position;
        cameraComponent = GetComponent<Camera>();
    }

    private void Update() {      

        //app. maxPlayerSpeed is 7.01

        float size = Mathf.MoveTowards(cameraComponent.orthographicSize, camMinSize + (camMaxSize - camMinSize) * (1 - (7.01f - v.magnitude) / 7.01f), 0.05f);
        cameraComponent.orthographicSize = size;


    }
    void FixedUpdate()
    {
        newPos = Player.transform.position;
        v = (newPos - oldPos) / Time.fixedDeltaTime;
        oldPos = newPos;
    }
}
