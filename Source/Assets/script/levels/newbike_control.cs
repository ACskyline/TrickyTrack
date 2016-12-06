using UnityEngine;
using System.Collections;

public class newbike_control : MonoBehaviour
{
    const float torque_break = 100;
    const float torque_wheel = 40;
    const float torque_body = 20;
    const float wheel_maxRSpeed = 3000;
    GameObject newwheel_rear;
    newplayer_control newplayer;
    public AudioClip engine;
    public AudioClip stall;
    public AudioClip brake;
    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        newwheel_rear = transform.FindChild("newwheel_rear").gameObject;
        newplayer = GameObject.Find("newplayer").GetComponent<newplayer_control>();
    }

    void Update()
    {
        if (newplayer != null)
        {
            if (newplayer.alive == false)
            {
                if (audioSource.isPlaying&&audioSource.clip==engine)
                {
                    audioSource.clip = null;
                    audioSource.PlayOneShot(stall);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    if (audioSource != null && engine != null && ((audioSource.isPlaying == false) || (audioSource.isPlaying == true && audioSource.clip != engine)))
                    {
                        audioSource.clip = engine;
                        audioSource.Play();
                    }
                    if (Mathf.Abs(newwheel_rear.rigidbody2D.angularVelocity) <= wheel_maxRSpeed)
                    {
                        newwheel_rear.rigidbody2D.AddTorque(-torque_wheel);
                    }
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    if (audioSource != null && stall != null && ((audioSource.isPlaying == false) || (audioSource.isPlaying == true && audioSource.clip != stall)))
                    {
                        audioSource.clip = stall;
                        audioSource.Play();
                    }
                }
                if (Input.GetKey(KeyCode.A))
                {
                    if (audioSource != null && stall != null && ((audioSource.isPlaying == false) || (audioSource.isPlaying == true && audioSource.clip != brake)))
                    {
                        audioSource.clip = brake;
                        audioSource.Play();
                    }
                    newwheel_rear.rigidbody2D.angularVelocity = 0;
                }
                if(Input.GetKeyUp(KeyCode.A))
                {
                    if(audioSource!=null&&audioSource.isPlaying)
                    {
                        audioSource.Stop();
                    }
                }
                if (Input.GetKey(KeyCode.E))
                {
                    rigidbody2D.AddTorque(-torque_body);
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    rigidbody2D.AddTorque(torque_body);
                }
            }
        }
    }
}