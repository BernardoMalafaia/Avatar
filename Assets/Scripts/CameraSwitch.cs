﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Camera1;
    public GameObject player;
    public Vector3 nextPos;
    public float speed = 50.0f;
    public bool lockPos = true;

    bool reverse = false;
    bool count = false;

    Player p;

    // Start is called before the first frame update
    void Start()
    {
        Camera1.SetActive(false);
        MainCamera.SetActive(true);
        p = player.GetComponent<Player>();
        p.canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((reverse == false) && (Camera1.active == true))
        {
            Camera1.transform.position = Vector3.MoveTowards(Camera1.transform.position, nextPos, speed * Time.deltaTime);
            if ((Camera1.transform.position == nextPos) && (lockPos == true))
            {
                reverse = true;
            }
        }
        else if ((reverse == true) && (Camera1.active == true))
        {
                Camera1.transform.position = Vector3.MoveTowards(Camera1.transform.position, MainCamera.transform.position, (speed * Time.deltaTime) * 4);
        }
        if ((Camera1.transform.position == MainCamera.transform.position) && (reverse == true))
        {
            
            Camera1.SetActive(false);
            MainCamera.SetActive(true);
            p.canMove = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            Player player = other.GetComponent<Player>();
            
            if (count == false)
            {
                if (lockPos == true)
                {
                    player.canMove = false;
                }
                
                reverse = false;

                Camera1.transform.position = MainCamera.transform.position;
                Camera1.SetActive(true);
                MainCamera.SetActive(false);
                
            }

            if (lockPos == true)
                count = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            reverse = true;
            if (lockPos == false)
                MainCamera.transform.position = Camera1.transform.position;
        }
    }
}
