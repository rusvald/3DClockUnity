using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsBehaviourScript : MonoBehaviour {
    float ypos = 0;
    bool createNew = true;
	// Use this for initialization
	void Start () {
        ypos = this.transform.position.y;
        float x = this.transform.position.x, y = this.transform.position.z, rot = Mathf.Acos(x / 4);
        rot = Vector3.Angle(new Vector3(0,0,4), new Vector3(x,0,y));

        this.transform.position = new Vector3(0, 0f + rot / 50.0f, 4);
        //this.transform.eulerAngles = new Vector3(0, rot, 0);
        this.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), rot);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(new Vector3(0,0,0), new Vector3(0,1,0), 1.0f);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 12.0f / 360.0f, this.transform.position.z);

        if(this.transform.eulerAngles.y > 10.0f && createNew)
        {
            GameObject.Instantiate(this.gameObject, new Vector3(0f, 0.0f, 4f), new Quaternion(0, 0, 0, 0));
            createNew = false;
        }
        if(this.transform.position.y > 12.0f)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }

        MeshRenderer meshR =  this.gameObject.GetComponent<MeshRenderer>();
        Material mat = meshR.materials[0];
        //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a + 0.1f);
        Color col = mat.GetColor("_Color");
        float rot = this.transform.eulerAngles.y > 0 ? this.transform.eulerAngles.y : 360.0f + this.transform.eulerAngles.y;
        mat.SetColor(Shader.PropertyToID("_Color"), new Color(col.r, col.g, col.b, 1.0f - rot / 320.0f));

        //col = mat.GetColor("_SpecColor");
        //mat.SetColor(Shader.PropertyToID("_SpecColor"), new Color(col.r, col.g, col.b, col.a > 0 ? col.a - 0.1f : 0));
        //Renderer r = gameObject.GetComponent<Renderer>();
    }
}
