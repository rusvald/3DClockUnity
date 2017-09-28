using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour {
    float a = 90;
    bool secondView = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(secondView && this.transform.eulerAngles.x > 45.0f)
        {
            this.transform.RotateAround(new Vector3(0,10,0), new Vector3(1, 0, 0), -1f);
            a -= 1f;
        }
        else if(!secondView && this.transform.eulerAngles.x < 90.0f)
        {
            this.transform.RotateAround(new Vector3(0, 10, 0), new Vector3(1, 0, 0), 1f);
            a += 1f;
        }
	}

    void ChangeView()
    {
        secondView = !secondView;
    }
}
