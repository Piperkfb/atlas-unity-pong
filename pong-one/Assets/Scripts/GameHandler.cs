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
    public TMP_Text timerText;
    public TMP_Text WhoWon;
    public float timer = 2.0f;
    private float spawntimer;
    public float spawnv1, spawnv2;
    public AudioClip SFXScore, SFXWin, SFXLose;
    public TMP_Text leftScore, rightScore;
    public GameObject WinMenu;
    public float specialBar = 0;
    private float leftScoreNumber, rightScoreNumber = 0;
    public Player PL;
    [HideInInspector] public bool specActive1 = false;
    [HideInInspector] public bool specActive2 = false;

    // Start is called before the first frame update
    void Start()
    {
        SoundFX = this.GetComponent<AudioSource>();
        spawntimer = Random.Range(spawnv1, spawnv2);
        Instantiate(ball, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        spawntimer -= Time.deltaTime;
        //check score
        //spawning
        if (spawntimer <= 0)
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(ball, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            spawntimer = Random.Range(spawnv1, spawnv2);
        }
        //360x to -360 by 360y to -360
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 RandSpawn = new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), 0);
            Instantiate(ball, transform.position + RandSpawn, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

        }
        //p2 or com
        //timer
        timer -= Time.deltaTime;
        timerText.text = string.Format("{0:0}:{1:00}", timer / 60, timer % 60);
        if (timer <= 0)
        {
            WinScreen();
        }
        
    }
    public void Scored()
    {
        //_ridgy.velocity = Vector2.zero;
        if (this.gameObject.transform.localPosition.x > 0)
        {
            leftScoreNumber++;
            leftScore.text = $"{leftScoreNumber}";
        }
        else if (this.gameObject.transform.localPosition.x < 0)
        {
            rightScoreNumber++;
            rightScore.text = $"{rightScoreNumber}";
        }
        SoundFX.clip = SFXScore;
        SoundFX.Play();
    }
    public void SpecialHandler()
    {
            if (specialBar == 0)
            {
                //negative sound
            }
            else if (specialBar < 5)
            {
                //sound effect;
                specActive1 = true;
                //visual display
            }
            else if (specialBar == 5)
            {
                //sound effect
                specActive2 = true;
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
