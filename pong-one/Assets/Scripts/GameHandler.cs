using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public GameObject P2, Com;
    private AudioSource SoundFX;
    public AudioSource Dragon;
    public GameObject ball, pollen, duck, golden, teleporter1, teleporter2;
    public TMP_Text timerText;
    public TMP_Text WhoWon;
    public float timer = 2.0f;
    private float Bspawntimer, Pspawntimer, Dspawntimer, Gspawntimer, Tspawntimer;
    public float ballspawnv1, ballspawnv2, pollenspawnv1, pollenspawnv2, duckspawnv1, duckspawnv2, 
                goldspawnv1, goldspawnv2, teleportspawnv1, teleportspawnv2;
    public AudioClip SFXWin, SFXLose, SFXNegSpecial, SFXSpec1, SFXSpec2;
    public AudioClip[] slurps;
    public TMP_Text leftScore, rightScore;
    public GameObject WinMenu;
    public float specialBarLeft, specialBarRight = 0;
    private float leftScoreNumber, rightScoreNumber = 0;
    public float specialTimerL, specialTimerR = 5.0f;
    public ParticleSystem Part1, Part2;
    public bool specActive1L = false;
    public bool specActive2L = false;
    public bool specActive1R = false;
    public bool specActive2R = false;
    public GameObject newball;

    // Start is called before the first frame update
    void Start()
    {
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
        SoundFX = this.GetComponent<AudioSource>();
        Bspawntimer = Random.Range(ballspawnv1, ballspawnv2);
        Pspawntimer = Random.Range(pollenspawnv1, pollenspawnv2);
        Dspawntimer = Random.Range(duckspawnv1, duckspawnv2);
        Gspawntimer = Random.Range(goldspawnv1, goldspawnv2);
        Tspawntimer = Random.Range(teleportspawnv1, teleportspawnv2);
        newball = Instantiate(ball, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        Pspawntimer -= Time.deltaTime;
        Bspawntimer -= Time.deltaTime;  
        Dspawntimer -= Time.deltaTime;
        Gspawntimer -= Time.deltaTime;
        Tspawntimer -= Time.deltaTime;
        //spawning
        if (Bspawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            newball = Instantiate(ball, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Bspawntimer = Random.Range(ballspawnv1, ballspawnv2);
        }
        if (Pspawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(pollen, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Pspawntimer = Random.Range(pollenspawnv1, pollenspawnv2);
        }
        if (Dspawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-590, 590), 605, 0);
            Instantiate(duck, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Dspawntimer = Random.Range(duckspawnv1, duckspawnv2);
        }
        if (Gspawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(golden, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            Gspawntimer = Random.Range(goldspawnv1, goldspawnv2);
            Dragon.Play();
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
        float minutes = Mathf.FloorToInt(timer / 60); 
        float seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        

        //left Spec2 
        if (specActive2L == true)
        {
            specialTimerL -= Time.deltaTime;
        }

        if (specialTimerL <= 0)
        {
            specActive2L = false;
            Part1.Stop();
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
            Part2.Stop();
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
        Ball DragonCheck = BBall.GetComponent<Ball>();
        if (BBall.transform.localPosition.x < 0)
        {
            if (DragonCheck.isDragon == true){
                leftScoreNumber += 3;
            }
            else{
                leftScoreNumber++;
            }

            leftScore.text = $"{leftScoreNumber}";
        }
        else if (BBall.transform.localPosition.x > 0)
        {
            if (DragonCheck.isDragon == true){
                rightScoreNumber += 3;
            }
            else{
                rightScoreNumber++;
            }
            rightScore.text = $"{rightScoreNumber}";
        }
        if (DragonCheck.isDragon == true)
        {
            Dragon.Stop();
        }
        SoundFX.clip = slurps[Random.Range(0, slurps.Length)];
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
                    SoundFX.clip = SFXSpec1;
                    SoundFX.Play();
                    specActive1L = true;
                    specialBarLeft -= 1;
                    //visual display
                }
                else if (specialBarLeft == 5)
                {
                    //sound effect
                    SoundFX.clip = SFXSpec2;
                    SoundFX.Play();
                    Part1.Play();
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
                    SoundFX.Play();
                    SoundFX.clip = SFXNegSpecial;
                }
                else if (specialBarRight < 5)
                {
                    //sound effect;
                    SoundFX.clip = SFXSpec1;
                    SoundFX.Play();
                    specActive1R = true;
                    specialBarRight -= 1;
                    //visual display
                }
                else if (specialBarRight == 5)
                {
                    //sound effect
                    SoundFX.clip = SFXSpec2;
                    SoundFX.Play();
                    Part2.Play();
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
