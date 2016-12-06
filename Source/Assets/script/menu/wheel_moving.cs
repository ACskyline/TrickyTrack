using UnityEngine;
using System.Collections;

public class wheel_moving : MonoBehaviour {
    private float spinspeed = 5000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, -spinspeed * Time.deltaTime);
	}
}
