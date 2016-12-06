using UnityEngine;
using System.Collections;

public class bike_moving : MonoBehaviour {

    private float xspeed = 10;
    private GameObject UImanager;
	// Use this for initialization
	void Start () {
        UImanager = GameObject.Find("UImanager");
        if(UImanager)
        {
            UImanager.SendMessage("syncBkg");
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(xspeed * Time.deltaTime, 0, 0);
	}
}
