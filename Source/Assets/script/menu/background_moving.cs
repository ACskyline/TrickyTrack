using UnityEngine;
using System.Collections;

public class background_moving : MonoBehaviour {
    private const float mywidth = 38.40f;
    private const float xspeed = 15;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.x <= -mywidth)
        {
            transform.localPosition = new Vector3(mywidth,transform.localPosition.y,transform.localPosition.z);
        }
        else
        {
            transform.Translate(-xspeed*Time.deltaTime, 0, 0);
        }
	}
}
