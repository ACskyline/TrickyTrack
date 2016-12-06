using UnityEngine;
using System.Collections;

public class floor_3_control : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Triggered()
    {
        Destroy(this.gameObject);
    }
}
