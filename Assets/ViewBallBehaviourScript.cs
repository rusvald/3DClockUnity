﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBallBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp()
    {
        UnityEngine.GameObject.Find("Main Camera").SendMessage("ChangeView");
    }
}
