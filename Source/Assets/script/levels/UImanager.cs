using UnityEngine;
using System.Collections;

public class UImanager : MonoBehaviour
{
    
    private const int MAXlevel = 16;
    private int state = 0;
    private int currentLevel = 1;
    private GameObject[] others;
    private float sound = 1;
    private float soundEX = 1;
    private AudioSource audioSource = null;
    public AudioClip bgm1;
    public AudioClip bgm2;
    private int bkg = 0;
    public Sprite[] bkgS;
    public GUISkin largeskin;
    public GUISkin smallskin;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        others = GameObject.FindGameObjectsWithTag("UImanager");//搜索其他UImanager
        if (others.Length > 1)//若有其他UImanager
        {
            Destroy(this.gameObject);//销毁自己
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            if(audioSource&&audioSource.clip!=null)
            {
                audioSource.Play();
            }
            syncBkg();
            Screen.SetResolution(1920, 1080, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                winLevel();
            }
        }
    }

    void OnGUI()
    {
        switch (state)
        {
            case 0://start
                GUI.skin = largeskin;
                if (GUI.Button(new Rect(Screen.width / 2 + 25 - 300, Screen.height - 100, 100, 50), "start"))
                {
                    state = 5;
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 25 - 150, Screen.height - 100, 100, 50), "manual"))
                {
                    state = 2;
                    Application.LoadLevel("manual");
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 25, Screen.height - 100, 100, 50), "setting"))
                {
                    state = 6;
                    Application.LoadLevel("setting");
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 25 + 150, Screen.height - 100, 100, 50), "quit"))
                {
                    state = 3;
                }
                break;
            case 1://play
                GUI.skin = smallskin;
                if (GUI.Button(new Rect(0, Screen.height - 20, 40, 20), "reset"))
                {
                    Application.LoadLevel("level" + currentLevel);
                }
                if (GUI.Button(new Rect(0 + 50, Screen.height - 20, 40, 20), "back"))
                {
                    state = 0;
                    Application.LoadLevel("menu");
                }
                if (GUI.Button(new Rect(0 + 100, Screen.height - 20, 40, 20), "quit"))
                {
                    state = 3;
                }
                break;
            case 2://manual
                GUI.skin = smallskin;
                if (GUI.Button(new Rect(0, Screen.height - 20, 40, 20), "back"))
                {
                    state = 0;
                    Application.LoadLevel("menu");
                }
                if (GUI.Button(new Rect(0 + 50, Screen.height - 20, 40, 20), "quit"))
                {
                    state = 3;
                }
                break;
            case 3://quit
                Application.Quit();
                break;
            case 4://ending
                GUI.skin = smallskin;
                if (GUI.Button(new Rect(0, Screen.height - 20, 40, 20), "back"))
                {
                    state = 0;
                    Application.LoadLevel("menu");
                }
                if (GUI.Button(new Rect(0 + 50, Screen.height - 20, 40, 20), "quit"))
                {
                    state = 3;
                }
                break;
            case 5://chose
                int margin = 50;
                int gap = 50;
                int perRow = (Screen.width-margin*2)/(100+gap);
                GUI.skin = largeskin;
                for (int i = 0; i < MAXlevel; i++)
                {
                    if (GUI.Button(new Rect(margin+i%(perRow)*(100+gap), Screen.height - 300+i/(perRow)*100, 100, 50), "level"+(i+1)))
                    {
                        currentLevel = i+1;
                        state = 1;
                        Application.LoadLevel("level" + currentLevel);
                    }
                }
                GUI.skin = smallskin;
                if (GUI.Button(new Rect(0, Screen.height - 20, 40, 20), "back"))
                {
                    state = 0;
                }
                if (GUI.Button(new Rect(0 + 50, Screen.height - 20, 40, 20), "quit"))
                {
                    state = 3;
                }
                break;
            case 6://setting
                GUI.skin = largeskin;
                sound = GUI.HorizontalSlider(new Rect(Screen.width / 2 - (50 + 150)*Screen.width/1024, Screen.height - (50)*Screen.height/576, 100, 10), sound, 0, 1);
                if (sound != soundEX)
                {
                    audioSource.volume = sound;
                    soundEX = sound;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (50 + 150)*Screen.width/1024, Screen.height - (150)*Screen.height/576, 100, 50), "BGM1"))
                {
                    if(audioSource)
                    {
                        audioSource.clip = bgm1;
                        audioSource.Play();
                    }
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (50)*Screen.width/1024, Screen.height - (150)*Screen.height/576, 100, 50), "BGM2"))
                {
                    if (audioSource)
                    {
                        audioSource.clip = bgm2;
                        audioSource.Play();
                    }
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (50 + 150) * Screen.width / 1024, 120*Screen.height/576, 100, 50), "BKG1"))
                {
                    bkg = 0;
                    syncBkg();
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 50 * Screen.width / 1024, 120 * Screen.height / 576, 100, 50), "BKG2"))
                {
                    bkg = 1;
                    syncBkg();
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (50 - 150) * Screen.width / 1024, 120 * Screen.height / 576, 100, 50), "BKG3"))
                {
                    bkg = 2;
                    syncBkg();
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (50 + 150) * Screen.width / 1024, 220 * Screen.height / 576, 100, 50), "1920*1080\nFull Screen"))
                {
                    Screen.SetResolution(1920, 1080, true);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 50 * Screen.width / 1024, 220 * Screen.height / 576, 100, 50), "1366*768\nWindow"))
                {
                    Screen.SetResolution(1366, 768, false);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - (50 - 150) * Screen.width / 1024, 220 * Screen.height / 576, 100, 50), "1024*576\nWindow"))
                {
                    Screen.SetResolution(1024, 576, false);
                }
                GUI.skin = smallskin;
                if (GUI.Button(new Rect(0, Screen.height - 20, 40, 20), "back"))
                {
                    state = 0;
                    Application.LoadLevel("menu");
                }
                if (GUI.Button(new Rect(0 + 50, Screen.height - 20, 40, 20), "quit"))
                {
                    state = 3;
                }
                break;
            default:
                break;
        }
    }

    private void syncBkg()
    {
        GameObject bkg1 = GameObject.Find("bkg1");
        if(bkg1)
        {
            SpriteRenderer spriteRenderer = bkg1.GetComponent<SpriteRenderer>();
            if(spriteRenderer)
            {
                spriteRenderer.sprite = bkgS[bkg];
            }
        }
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    void winLevel()
    {
        currentLevel++;
        if (currentLevel >= 1 && currentLevel <= MAXlevel)
        {
            state = 1;
            Application.LoadLevel("level" + currentLevel);
        }
        else if (currentLevel == MAXlevel + 1)
        {
            currentLevel = 0;
            state = 4;
            Application.LoadLevel("ending");
        }
    }

    void loseLevel()
    {
        if (currentLevel >= 1 && currentLevel <= MAXlevel)
        {
            state = 1;
            Application.LoadLevel("level" + currentLevel);
        }
    }
}