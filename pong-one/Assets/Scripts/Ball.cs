using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Data.Common;


public class Ball : MonoBehaviour
{
    private AudioSource SoundFX;
    public AudioClip SFXPad, SFXWall, SFXScore, SFXWin, SFXLose;
    private Rigidbody2D _ridgy; 
    private RectTransform _ridgypos;
    public float speed;
    public GameObject scoreMenu;
    public GameObject WinMenu;
    public ScreenShaker BG;
    public GameObject P2, Com;
    public TMP_Text leftScore, rightScore;
    private float leftScoreNumber, rightScoreNumber = 0;
    public TMP_Text whoScored, WhoWon;
    private Vector2 BallResetPos;
    private Animator Anime;
    private RectTransform flipper;
    public float healthBub = 3;
    private bool popped = false;
    public Image flyimg;
    public GameObject Bub;
    
    private void Awake()
    {
        _ridgy = GetComponent<Rigidbody2D>();
        _ridgypos = GetComponent<RectTransform>();
        BallResetPos = _ridgypos.transform.position;
        Anime = GetComponentInChildren<Animator>();
        flipper = transform.Find("FlipParent").GetComponent<RectTransform>();
        flyimg = transform.Find("FlipParent/Fly").GetComponent<Image>();
        BG.GetComponent<ScreenShaker>();
    }

    private void Start()
    {
        
        AddStartingForce();
        SoundFX = this.GetComponent<AudioSource>();
        if (ModeMenu.ComOn == true)
        {
            P2.SetActive(false);
            Com.SetActive(true);
        }
        else if (ModeMenu.ComOn == false)
        {
            P2.SetActive(true);
            Com.SetActive(false);
        }
    }



    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        Vector2 direction = new Vector2(x,y);

        _ridgy.AddForce(direction * this.speed * 3);


        //fly facing
        if (direction.x > 0)
            flipper.transform.eulerAngles = (new Vector3(0, 180, 0));
        else
            flipper.transform.eulerAngles = (new Vector3(0, 0, 0));    
    }
    private void Update()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Paddle"))
        {
            if (popped == true)
            {
                Scored();
            }
            else
            {
                RectTransform paddlepos = boink.gameObject.GetComponent<RectTransform>();
                
                //calculate angle
                float y = launchAngle(AnchorPos(), paddlepos.anchoredPosition, paddlepos.sizeDelta.y / 2f);

                //set angle and speed
                float x = _ridgy.velocity.x < 0 ? 1.0f : -1.0f;
                Vector2 d = new Vector2(x, y).normalized;
                //_ridgy.velocity = d * this.speed * 1.5F;
                
                //direction.y = -direction.y;
                _ridgy.velocity = Vector2.zero;
                _ridgy.AddForce(d * this.speed * 3);
                //Sound FX
                SoundFX.clip = SFXPad;
                SoundFX.Play();
                //animations
                if (d.x > 0)
                    flipper.transform.eulerAngles = (new Vector3(0, 180, 0));
                else 
                    flipper.transform.eulerAngles = (new Vector3(0, 0, 0));

                //-1 bubble health
                healthBub -= 1;
                BubbleHealth();
            }
        } 
        if (boink.gameObject.CompareTag("Wall"))
        {
            
            Vector2 VelSave = _ridgy.velocity;
            VelSave.y = -VelSave.y;
            _ridgy.velocity = VelSave;
            //sound FX
            SoundFX.clip = SFXWall;
            SoundFX.Play();
            //anmiation
            Anime.SetTrigger("WallHit");
            //screen shake
            BG.ScreenShakeForTime(0.5f);
        }


    }
    private void Scored()
    {
        Debug.Log ("Goal!!");
        _ridgy.velocity = Vector2.zero;
        if (this.gameObject.transform.localPosition.x > 0)
        {
            leftScoreNumber++;
            leftScore.text = $"{leftScoreNumber}";
            whoScored.text = $"P1";
        }
        else if (this.gameObject.transform.localPosition.x < 0)
        {
            rightScoreNumber++;
            rightScore.text = $"{rightScoreNumber}";
            whoScored.text = $"P2";
        }
        SoundFX.clip = SFXScore;
        SoundFX.Play();
        if (leftScoreNumber == 11 || rightScoreNumber == 11)
        {
            WinScreen();
        }
        else
        {
            scoreMenu.SetActive(true);

            Invoke("ResetPos", 2.0f);
            Invoke("AddStartingForce", 3.0f);
        }
    }
    private void BubblePop()
    {
        //reduce collider to fly size
        CircleCollider2D Circ = GetComponent<CircleCollider2D>();
        Circ.radius = 4;
        //make not transparent anymore

        flyimg.color = new Color(1f, 1f, 1f, 1f);
        //remove bubble after pop
        //Bub.SetActive(false);
        Anime.SetTrigger("Popped");
        //set fly as scorable
        popped = true;
    }
    private void BubbleHealth()
    {
        if (this.healthBub == 0)
        {
            BubblePop();
        }
    }
    private void WinScreen()
    {
        //pause paddles
        
        GameObject[] allpaddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach(GameObject paddles in allpaddles)
        {
            Rigidbody2D padrid = paddles.GetComponent<Rigidbody2D>();
            padrid.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        if (leftScoreNumber == 11)
        {
            WhoWon.text = $"P1";
            SoundFX.clip = SFXWin;
            SoundFX.Play();
        }
        else if (rightScoreNumber == 11)
        {
            WhoWon.text = $"P2";
            SoundFX.clip = SFXLose;
            SoundFX.Play();
        }
        WinMenu.SetActive(true);
        SoundFX.Play();
        Invoke("PlayAgain", 2);
        //display win, replay menu
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("PlayAgain");
    }
    float launchAngle(Vector2 ball, Vector2 paddle, float paddleHeight) 
    {
        return (ball.y - paddle.y) / paddleHeight;
    }
    public Vector2 AnchorPos()
    {
        return _ridgypos.anchoredPosition;
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
    private void ResetPos()
    {

        Bub.SetActive(true);
        CircleCollider2D Circ = GetComponent<CircleCollider2D>();
        Circ.radius = 10;
        flyimg.color = new Color(1f, 1f, 1f, 0.5f);
        _ridgypos.transform.position = BallResetPos;
        scoreMenu.SetActive(false);
    }
}
