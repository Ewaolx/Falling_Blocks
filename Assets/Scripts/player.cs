using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public event Action onPlayerDestroyed;
    float screenHalfWidth;
    float speed = 5;
	// Use this for initialization
	void Start () {
        //print("Screen :" + Screen.width);	
        screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        // print("Output:"+input);
         Vector3 dir = input.normalized;
         Vector3 vel = dir * speed;
         transform.Translate(vel*Time.deltaTime);

        Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
        if (tmpPos.x > Screen.width || tmpPos.x < 0)
        {
            Vector3 pos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.position = pos;
            print("p:");
        }
        /*if (transform.position.x > screenHalfWidth || transform.position.x < -screenHalfWidth)
        {
            Vector3 pos = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
            transform.position = pos;
            print("p:");
        }*/
        
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        print("Entering function");
        if (triggerCollider.tag == "obstacleBlocks")
        {
            Destroy(gameObject);
            if(onPlayerDestroyed != null)
               onPlayerDestroyed();
        }
    }
}
