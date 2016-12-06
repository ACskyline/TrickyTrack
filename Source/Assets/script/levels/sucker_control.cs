using UnityEngine;
using System.Collections;

public class sucker_control : MonoBehaviour {
    public GameObject other_sucker;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "newbike_body" || other.name == "rock")
        {
            other.transform.position = other_sucker.transform.position + (other.transform.position - transform.position);
        }
    }
}
