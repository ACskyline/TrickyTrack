using UnityEngine;
using System.Collections;

public class camera_control : MonoBehaviour {
    public Transform cameraTarget;
    public Transform lefthandle;
    public Transform righthandle;
    public Transform uphandle;
    public Transform downhandle;
    public float smoothTime = 0.1f;
    public bool follow;
    private GameObject manualTemp;
    private Vector2 velocity;

    void Start()
    {
        manualTemp = transform.FindChild("manualTemp").gameObject;
    }

    void Update()
    {
        //Debug.Log("xdiff:" + Mathf.Abs(cameraTarget.transform.position.x - transform.position.x) + " ydiff:" + Mathf.Abs(cameraTarget.transform.position.y - transform.position.y));
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(manualTemp)
            {
                manualTemp.SetActive(!manualTemp.activeSelf);
            }
        }
        if (follow)
        {
            if (transform.position.x < lefthandle.position.x)
            {
                transform.position = new Vector3(lefthandle.position.x, transform.position.y, transform.position.z);
            }
            if (transform.position.x > righthandle.position.x)
            {
                transform.position = new Vector3(righthandle.position.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y < downhandle.position.y)
            {
                transform.position = new Vector3(transform.position.x, downhandle.position.y, transform.position.z);
            }
            if (transform.position.y > uphandle.position.y)
            {
                transform.position = new Vector3(transform.position.x, uphandle.position.y, transform.position.z);
            }
        }
    }

    void FixedUpdate()
    {
        if (follow)
        {
            if (cameraTarget != null)
            {
                Vector3 newPos = transform.position;
                newPos.x = Mathf.SmoothDamp(transform.position.x, cameraTarget.position.x, ref velocity.x, smoothTime);
                newPos.y = Mathf.SmoothDamp(transform.position.y, cameraTarget.position.y, ref velocity.y, smoothTime);
                transform.position = newPos;
            }
        }
    }
}
