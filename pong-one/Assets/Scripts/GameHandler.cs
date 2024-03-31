using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private AudioSource SoundFX;
    public GameObject ball;
    public GameObject pollen;
    public Player PL;
    public TMP_Text timerText;
    public TMP_Text WhoWon;
    public float timer = 2.0f;
    private float spawntimer;
    private float Pspawntimer;
    public float ballspawnv1, ballspawnv2;
    public float pollenspawnv1, pollenspawnv2;
    public AudioClip SFXScore, SFXWin, SFXLose, SFXSpecial, SFXNegSpecial;
    public TMP_Text leftScore, rightScore;
    public GameObject WinMenu;
    public float specialBarLeft, specialBarRight = 0;
    private float leftScoreNumber, rightScoreNumber = 0;
    public float specialTimerL, specialTimerR = 5.0f;
    public bool isleft;
    [HideInInspector] public bool specActive1L = false;
    [HideInInspector] public bool specActive2L = false;
    [HideInInspector] public bool specActive1R = false;

    [HideInInspector] public bool specActive2R = false;


    // Start is called before the first frame update
    void Start()
    {
        SoundFX = this.GetComponent<AudioSource>();
        spawntimer = Random.Range(ballspawnv1, ballspawnv2);
        Pspawntimer = Random.Range(pollenspawnv1, pollenspawnv2);
        Instantiate(ball, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        Pspawntimer -= Time.deltaTime;
        spawntimer -= Time.deltaTime;
        //check score
        //spawning
        if (spawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(ball, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            spawntimer = Random.Range(ballspawnv1, ballspawnv2);
        }
        if (Pspawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(pollen, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Pspawntimer = Random.Range(pollenspawnv1, pollenspawnv2);
        }
        //360x to -360 by 360y to -360

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(pollen, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            
        }

        //p2 or com
        //timer

        timer -= Time.deltaTime;
        timerText.text = string.Format("{0:0}:{1:00}", timer / 60, timer % 60);
        

        //left Spec2 
        if (specActive2L == true)
        {
            specialTimerL -= Time.deltaTime;
        }

        if (specialTimerL <= 0)
        {
            specActive2L = false;
            specialTimerL = 5.0f;
        }

        //right Spec2
        if (specActive2R == true)
        {
            specialTimerR -= Time.deltaTime;
        }

        if (specialTimerR <= 0)
        {
            specActive2R = false;
            specialTimerR = 5.0f;
        }
        if (timer <= 0)
        {
            WinScreen();
        }
        
    }
    public void Scored(GameObject BBall)
    {
        //_ridgy.velocity = Vector2.zero;
        if (BBall.transform.localPosition.x < 0)
        {
            leftScoreNumber++;
            leftScore.text = $"{leftScoreNumber}";
        }
        else if (BBall.transform.localPosition.x > 0)
        {
            rightScoreNumber++;
            rightScore.text = $"{rightScoreNumber}";
        }
        SoundFX.clip = SFXScore;
        SoundFX.Play();
    }
    public void SpecialHandler(bool isleft)
    {
        if (isleft == true)
        {   
            if (specActive1L == false)
            {
                if (specialBarLeft == 0)
                {
                    //negative sound
                    SoundFX.clip = SFXNegSpecial;
                    SoundFX.Play();
                }
                else if (specialBarLeft < 5)
                {
                    //sound effect;
                    SoundFX.clip = SFXSpecial;
                    SoundFX.Play();
                    specActive1L = true;
                    specialBarLeft -= 1;
                    //visual display
                }
                else if (specialBarLeft == 5)
                {
                    //sound effect
                    specActive2L = true;
                    specialBarLeft = 0;
                }
            }
        }
        else if (isleft == false)
        {
            if (specActive1R == false)
            {
                if (specialBarRight == 0)
                {
                    //negative sound
                    SoundFX.clip = SFXNegSpecial;
                }
                else if (specialBarRight < 5)
                {
                    //sound effect;
                    SoundFX.clip = SFXSpecial;
                    specActive1R = true;
                    specialBarRight -= 1;
                    //visual display
                }
                else if (specialBarRight == 5)
                {
                    //sound effect
                    specActive2R = true;
                    specialBarRight = 0;
                }
            }
        }
    }
    private void WinScreen()
    {
        //pause paddles
        Time.timeScale = 0;
        GameObject[] allpaddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach(GameObject paddles in allpaddles)
        {
            Rigidbody2D padrid = paddles.GetComponent<Rigidbody2D>();
            padrid.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        if (leftScoreNumber > rightScoreNumber)
        {
            WhoWon.text = $"P1";
            SoundFX.clip = SFXWin;
            SoundFX.Play();
        }
        else
        {
            WhoWon.text = $"P2";
            SoundFX.clip = SFXLose;
            SoundFX.Play();
        }
        WinMenu.SetActive(true);
        SoundFX.Play();
        Invoke("PlayAgain", 2);
        Time.timeScale = 1;
        //display win, replay menu
    }
        public void PlayAgain()
    {
        SceneManager.LoadScene("PlayAgain");
    }
}
