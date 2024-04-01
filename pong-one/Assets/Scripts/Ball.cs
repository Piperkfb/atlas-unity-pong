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
using System;
using Random = UnityEngine.Random;


public class Ball : MonoBehaviour
{
    public Color slowCol, midCol, fastCol, Health2, Health1;
    private AudioSource SoundFX;
    public AudioClip SFXPad, SFXWall;
    [HideInInspector] public Rigidbody2D _ridgy; 
    private RectTransform _ridgypos;
    public GameObject daFly;
    public Vector2 SpeedIndicat_1;
    public float speed;
    public ScreenShaker BG;
    public GameHandler GH;
    public GameObject currentportal;
    private Vector2 BallResetPos;
    private Animator Anime;
    private RectTransform flipper;
    public float healthBub = 3;
    private bool popped = false;
    public Image flyimg;
    public Image Bub;
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
            bool lefty = boink.GetComponent<Player>().isleft;
            if (popped == true)
            {
                GH.Scored(daFly);
                Destroy(daFly);
            }
            else if ((GH.specActive2L == true && lefty == true) ||
                    (GH.specActive2R == true && lefty == false))
            {

                GH.Scored(daFly);
                Destroy(daFly);
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
                if (GH.specActive1L == true && lefty == true)
                {
                    healthBub -= 2;
                    GH.specActive1L = false;
                    BubbleHealth();
                }
                else if (GH.specActive1R == true && lefty == false)
                {
                    healthBub -= 2;
                    GH.specActive1R = false;
                    BubbleHealth();
                }
                else
                {
                    healthBub -= 1;
                    BubbleHealth();

                }
                if (healthBub == 2)
                {
                    Bub.GetComponent<Image>().color = Health2;
                }
                else if (healthBub == 1)
                {
                    Bub.GetComponent<Image>().color = Health1;
                }
            }
        } 
        if (boink.gameObject.CompareTag("Wall"))
        {
            Vector2 VelSave = _ridgy.velocity;
            float yneg = VelSave.y < 0 ? 1.0f : -1.0f;
            VelSave.y = -VelSave.y + yneg;
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
            float xneg = VelSave.x < 0 ? 1.0f : -1.0f;
            VelSave.x = -VelSave.x + xneg;
            _ridgy.velocity = VelSave;
        }
        if (boink.gameObject.CompareTag("Ball"))
        {
            Vector2 VelSave = _ridgy.velocity;
            float xneg = VelSave.x < 0 ? 1.0f : -1.0f;
            float yneg = VelSave.y < 0 ? 1.0f : -1.0f;
            VelSave.x = -VelSave.x + xneg;
            VelSave.y = -VelSave.y + xneg;
            _ridgy.velocity = VelSave;
        }
        if (boink.gameObject.CompareTag("Duck"))
        {
            // get the direction of the collision
            Vector3 direction = transform.position - boink.gameObject.transform.position;
            // see if the obect is futher left/right or up down
            if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) 
            {
                Vector2 VelSave = _ridgy.velocity;
                float xneg = VelSave.x < 0 ? 1.0f : -1.0f;
                VelSave.x = -VelSave.x + xneg;
                _ridgy.velocity = VelSave;

            }
            else
            {
                Vector2 VelSave = _ridgy.velocity;
                float yneg = VelSave.y < 0 ? 1.0f : -1.0f;
                VelSave.y = -VelSave.y + yneg;
                _ridgy.velocity = VelSave;
            }	
	    }
        if (boink.gameObject.CompareTag("Portal"))
        {
            if (currentportal == null)
            {
                currentportal = boink.gameObject;
                transform.position = currentportal.GetComponent<Portal>().GetDestination().position;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D bop) 
    {
        if (bop.gameObject.CompareTag("Portal"))
        {
            if (bop.gameObject != currentportal)
            {
                currentportal = null;
            }
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
            GH.Scored(daFly);
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

}
