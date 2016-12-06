using UnityEngine;
using System.Collections;

public class camera_focus : MonoBehaviour {

    public Transform FollowCenter;
    public float x_offset;
    public float y_offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (FollowCenter != null)
        {
            transform.position = FollowCenter.position + new Vector3(x_offset, y_offset, 0);
        }
	}
}
