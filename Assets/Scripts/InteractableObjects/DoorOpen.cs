﻿using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {

    public bool open;
    public int moving;
    public GameObject player;
    public GameObject door;
    public float disMoved;

	// Use this for initialization
	void Start () {
        open = false;
        moving = 0;
        disMoved = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        float currDist = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - door.transform.position.x, 2) + Mathf.Pow(player.transform.position.z - door.transform.position.z, 2));
        if (currDist < 1 && !open)
        {
            moving = 1;
        } else if (currDist > 3 && open)
        {
            moving = -1;
        }
        if (moving == 1 && !open)
        {
            door.transform.Translate(new Vector3(0.01f, 0.0f, 0.0f));
            disMoved += .01f;
            if (disMoved >= 1.0f)
            {
                open = true;
                disMoved = 0.0f;
            }
        }
        else if (moving == -1 && open)
        {
            door.transform.Translate(new Vector3(-0.01f, 0.0f, 0.0f));
            disMoved -= .01f;
            if (disMoved <= -1.0f)
            {
                open = false;
                disMoved = 0.0f;
            }
        }
	} 
}
