using UnityEngine;
using System.Collections;

public class antizone_control : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.rigidbody2D)
        {
            other.rigidbody2D.gravityScale = -other.rigidbody2D.gravityScale;
        }
    }
}
