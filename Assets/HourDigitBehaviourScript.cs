using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourDigitBehaviourScript : MonoBehaviour {
    bool enable_anim = true;
    int hourDig = 0, secOld = -1, sec10Old = -1, minOld = -1, min10Old = -1, hrOld = -1, hr10Old = -1;
    float pos = 0, digSpd = 0.4f;
    bool isInstance = false;
    string digType = "sec";

	// Use this for initialization
	void Start () {
        if (isInstance) return;
        for(int i =0; i < 10; i++)
        {
            if (string.Format("Hour_{0}", i) == this.gameObject.name)
            {
                hourDig = i;
                break;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        int sec10 = System.DateTime.Now.TimeOfDay.Seconds / 10;
        int sec = System.DateTime.Now.TimeOfDay.Seconds - sec10 * 10;
        int min10 = System.DateTime.Now.TimeOfDay.Minutes / 10;
        int min = System.DateTime.Now.TimeOfDay.Minutes - min10 * 10;
        int hr10 = System.DateTime.Now.TimeOfDay.Hours / 10;
        int hr = System.DateTime.Now.TimeOfDay.Hours - hr10 * 10;

        if (!enable_anim) return;

        if (!isInstance)
        {
            if (sec == hourDig && sec != secOld)
            {
                GameObject gobj = Instantiate(this.gameObject);
                gobj.transform.position = new Vector3(gobj.transform.position.x + 0.5f, gobj.transform.position.y, gobj.transform.position.z);
                gobj.SendMessage("SetInstance", new DigitState() { Type = "sec", Digit = hourDig });
                secOld = sec;
            }
            else if (sec != hourDig)
            {
                secOld = -1;
            }
            if (sec10 == hourDig && sec10 != sec10Old)
            {
                GameObject gobj = Instantiate(this.gameObject);
                gobj.transform.position = new Vector3(gobj.transform.position.x - 0.5f, gobj.transform.position.y, gobj.transform.position.z);
                gobj.SendMessage("SetInstance", new DigitState() { Type = "sec10", Digit = hourDig });
                sec10Old = sec10;
            }
            else if (sec10 != hourDig)
            {
                sec10Old = -1;
            }

            //Minutes
            if (min == hourDig && min != minOld)
            {
                GameObject gobj = Instantiate(this.gameObject);
                gobj.transform.position = new Vector3(gobj.transform.position.x - 1.5f, gobj.transform.position.y, gobj.transform.position.z);
                gobj.SendMessage("SetInstance", new DigitState() { Type = "min", Digit = hourDig });
                minOld = min;
            }
            else if (min != hourDig)
            {
                minOld = -1;
            }
            if (min10 == hourDig && min10 != min10Old)
            {
                GameObject gobj = Instantiate(this.gameObject);
                gobj.transform.position = new Vector3(gobj.transform.position.x - 2.5f, gobj.transform.position.y, gobj.transform.position.z);
                gobj.SendMessage("SetInstance", new DigitState() { Type = "min10", Digit = hourDig });
                min10Old = min10;
            }
            else if (min10 != hourDig)
            {
                min10Old = -1;
            }

            //Hours
            if (hr == hourDig && hr != hrOld)
            {
                GameObject gobj = Instantiate(this.gameObject);
                gobj.transform.position = new Vector3(gobj.transform.position.x - 3.5f, gobj.transform.position.y, gobj.transform.position.z);
                gobj.SendMessage("SetInstance", new DigitState() { Type = "hr", Digit = hourDig });
                hrOld = hr;
            }
            else if (hr != hourDig)
            {
                hrOld = -1;
            }
            if (hr10 == hourDig && hr10 != hr10Old)
            {
                GameObject gobj = Instantiate(this.gameObject);
                gobj.transform.position = new Vector3(gobj.transform.position.x - 4.5f, gobj.transform.position.y, gobj.transform.position.z);
                gobj.SendMessage("SetInstance", new DigitState() { Type = "hr10", Digit = hourDig });
                hr10Old = hr10;
            }
            else if (hr10 != hourDig)
            {
                hr10Old = -1;
            }
            return;
        }

        switch (digType)
        {
            case "hr10":
                MoveDigit(hr10);
                break;
            case "hr":
                MoveDigit(hr);
                break;
            case "min10":
                MoveDigit(min10);
                break;
            case "min":
                MoveDigit(min);
                break;
            case "sec10":
                MoveDigit(sec10);
                break;
            case "sec":
            default:
                MoveDigit(sec);
                break;
        }
	}

    public void SetInstance(DigitState type)
    {
        isInstance = true;
        digType = type.Type;
        hourDig = type.Digit;
    }

    private void MoveDigit(int timeNum)
    {
        int sec10 = timeNum / 10;
        int sec = timeNum - sec10 * 10;
        float k = 90.0f * digSpd / 10.0f;

        if (sec == hourDig && pos < 10.0f)
        {
            pos += digSpd;
            this.gameObject.transform.Translate(new Vector3(0, -digSpd, 0));
            this.gameObject.transform.Rotate(new Vector3(0, 1, 0), k);
        }
        else if (sec != hourDig && pos > 0.0f)
        {
            this.gameObject.transform.Translate(new Vector3(0, pos, 0));
            this.gameObject.transform.Rotate(new Vector3(0, -1, 0), 90.0f);
            pos -= pos;
            
            Destroy(this.gameObject);
        }
    }

    public struct DigitState
    {
        public string Type { get; set; }
        public int Digit { get; set; }
    }
}
