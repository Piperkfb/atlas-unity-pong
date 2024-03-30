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
    public Color slowCol, midCol, fastCol;
    private AudioSource SoundFX;
    public AudioClip SFXPad, SFXWall;
    private Rigidbody2D _ridgy; 
    private RectTransform _ridgypos;
    public GameObject daFly;
    public Vector2 SpeedIndicat_1;
    public float speed;
    public ScreenShaker BG;
    public GameHandler GH;
    public GameObject P2, Com;
    private Vector2 BallResetPos;
    private Animator Anime;
    private RectTransform flipper;
    public float healthBub = 3;
    private bool popped = false;
    public Image flyimg;
    public GameObject Bub;
    private TrailRenderer myTrailRenderer;
    public Player PL;
    
    private void Awake()
    {
        _ridgy = GetComponent<Rigidbody2D>();
        _ridgypos = GetComponent<RectTransform>();
        BallResetPos = _ridgypos.transform.position;
        Anime = GetComponentInChildren<Animator>();
        flipper = transform.Find("FlipParent").GetComponent<RectTransform>();
        flyimg = transform.Find("FlipParent/Fly").GetComponent<Image>();
        BG.GetComponent<ScreenShaker>();
        myTrailRenderer = GetComponent<TrailRenderer>();
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
    private void FixedUpdate() 
    {
        Vector2 Abbvec = AbsVec(_ridgy.velocity);
        //speed checking
        if (Abbvec.x <= 300 || Abbvec.y <= 300)
        {
            myTrailRenderer.material.color = slowCol;
        }
        else if ((Abbvec.x > 300 || Abbvec.y > 300) && (Abbvec.x <= 600 || Abbvec.y <= 600))
        {
            myTrailRenderer.material.color = midCol;
        }
        else if (Abbvec.x > 600 || Abbvec.y > 600)
        {
            myTrailRenderer.material.color = fastCol; 
        }
    }
    private void Update()
    {

        //fly facing
        if (_ridgy.velocity.x > 0)
            flipper.transform.eulerAngles = (new Vector3(0, 180, 0));
        else
            flipper.transform.eulerAngles = (new Vector3(0, 0, 0));    
    }

    protected void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Paddle"))
        {
            if (popped == true)
            {
                GH.Scored();
                Destroy(daFly);
            }
            else if (GH.specActive2) 
            {

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
                _ridgy.AddForce(d * this.speed * 4);
                //Sound FX
                SoundFX.clip = SFXPad;
                SoundFX.Play();
                //animations
                if (d.x > 0)
                    flipper.transform.eulerAngles = (new Vector3(0, 180, 0));
                else 
                    flipper.transform.eulerAngles = (new Vector3(0, 0, 0));

                //-1 bubble health
                if (GH.specActive1 == true)
                {
                    healthBub -= 2;
                    GH.specActive1 = false;
                }
                else
                {
                    healthBub -= 1;
                    BubbleHealth();
                }
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
            //BG.ScreenShakeForTime(0.5f);
        }
        if (boink.gameObject.CompareTag("Goal"))
        {
            Vector2 VelSave = _ridgy.velocity;
            Debug.Log(VelSave);
            float xneg = VelSave.x < 0 ? 1.0f : -1.0f;
            float yneg = VelSave.y < 0 ? 1.0f : -1.0f;
            VelSave.x = -VelSave.x + xneg;
            VelSave.y = -VelSave.y + xneg;
            _ridgy.velocity = VelSave;
            Debug.Log(_ridgy.velocity);
            
        }
        if (boink.gameObject.CompareTag("Ball"))
        {
            Vector2 VelSave = _ridgy.velocity;
            float xneg = VelSave.x < 0 ? 1.0f : -1.0f;
            float yneg = VelSave.y < 0 ? 1.0f : -1.0f;
            VelSave.x = -VelSave.x + xneg;
            VelSave.y = -VelSave.y + xneg;
            _ridgy.velocity = VelSave;
            Debug.Log(_ridgy.velocity);
        }

    }
    
    private void BubblePop()
    {
        //reduce collider to fly size
        CircleCollider2D Circ = GetComponent<CircleCollider2D>();
        Circ.radius = 4;
        //make not transparent anymore

        flyimg.color = new Color(1f, 1f, 1f, 1f);
        Anime.SetTrigger("Popped");
        //set fly as scorable
        popped = true;
    }
    private void BubbleHealth()
    {   
        if (this.healthBub < 0)
        {
            GH.Scored();
            Destroy(daFly);
        }
        else if (this.healthBub == 0)
        {
            BubblePop();
        }

    }
    

    float launchAngle(Vector2 ball, Vector2 paddle, float paddleHeight) 
    {
        return (ball.y - paddle.y) / paddleHeight;
    }
    public Vector2 AnchorPos()
    {
        return _ridgypos.anchoredPosition;
    }
    public Vector2 AbsVec(Vector2 v2)
    {
        return new Vector2 (Mathf.Abs(v2.x), Mathf.Abs(v2.y));
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
        //scoreMenu.SetActive(false);
    }
}
