using UnityEngine;
using System.Collections;

public class newplayer_control : MonoBehaviour
{

    Rigidbody2D[] parts;
    Rigidbody2D newbike_body;
    GameObject UImanager;
    public bool alive = true;
    public AudioClip snapNeck;
    public AudioClip reach;
    AudioSource audioSource;
    private bool won = false;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parts = transform.GetComponentsInChildren<Rigidbody2D>();
        newbike_body = GameObject.Find("newbike_body").rigidbody2D;
        UImanager = GameObject.Find("UImanager");
        if (UImanager)
        {
            UImanager.SendMessage("syncBkg");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (transform.position.y < -30 || transform.position.y > 40)
            {
                alive = false;
                Invoke("lose", 2);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alive&&!won)
        {
            if (other.tag == "reach")
            {
                if (audioSource != null && reach != null)
                {
                    audioSource.PlayOneShot(reach);
                }
                won = true;
                Invoke("win", 2);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive && audioSource != null && snapNeck != null)
        {
            audioSource.PlayOneShot(snapNeck);
        }
        transform.parent = null;
        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].isKinematic = false;
        }
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].name == "newplayer_body")
            {
                parts[i].velocity = newbike_body.velocity;
            }
        }
        if (alive)
        {
            alive = false;
            Invoke("lose", 2);
        }
    }

    void win()
    {
        Debug.Log("you win");
        if (UImanager != null)
        {
            UImanager.SendMessage("winLevel");//调用UImanager的函数
        }
        else
        {
            Application.LoadLevel(Application.loadedLevelName);
        }

    }

    void lose()
    {
        Debug.Log("you lose");
        if (UImanager != null)
        {
            UImanager.SendMessage("loseLevel");//调用UImanager的函数
        }
        else
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }

}
