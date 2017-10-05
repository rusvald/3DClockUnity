using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBarBehaviourScript : MonoBehaviour {
    float ay = 0, az = 0;
    bool enable_anim = true;

	// Use this for initialization
	void Start () {
        ay = this.transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (!enable_anim) return;
        this.transform.eulerAngles = new Vector3(0, ay, az);
        
        this.transform.position =new Vector3(this.transform.position.x, this.transform.position.y + 0.01f, this.transform.position.z);
        ay += 1.0f;
        az += 2.0f;
        if (ay > 360.0f) ay = ay - 360.0f;
        if (az >= 180.0f) az = -180.0f + az - 180.0f;

        MeshRenderer meshR = this.gameObject.GetComponent<MeshRenderer>();
        Material mat = meshR.materials[0];
        Color col = mat.GetColor("_Color");
        if(this.transform.position.y > 7.8f)
            mat.SetColor(Shader.PropertyToID("_Color"), new Color(col.r, col.g, col.b, 1.0f - (this.transform.position.y - 7.8f) / 1.0f));
        else if (this.transform.position.y < 6.0f)
            mat.SetColor(Shader.PropertyToID("_Color"), new Color(col.r, col.g, col.b, (this.transform.position.y - 5.2f) / 0.8f));
        else
            mat.SetColor(Shader.PropertyToID("_Color"), new Color(col.r, col.g, col.b, 1.0f));

        if (this.transform.position.y > 8.8f)
            this.transform.position = new Vector3(this.transform.position.x, 5.2f, this.transform.position.z);
    }
}
