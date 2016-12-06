using UnityEngine;
using System.Collections;

public class trigger_control : MonoBehaviour {
    public GameObject targetObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetObj != null)
        {
            targetObj.SendMessage("Triggered");
        }
    }
}
