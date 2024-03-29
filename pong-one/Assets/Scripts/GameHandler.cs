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
    public AudioClip SFXScore, SFXWin, SFXLose;
    public TMP_Text leftScore, rightScore;
    public GameObject WinMenu;
    private float leftScoreNumber, rightScoreNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        SoundFX = this.GetComponent<AudioSource>();
        spawntimer = Random.Range(4, 10);
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
            spawntimer = Random.Range(4, 10);
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
            GameOver();
        }
    }
    private void GameOver()
    {
        //pause game
        Time.timeScale = 0;
            //time.scaletime
        //display end of game
        //display who won?
        //play again
    }
    public void Scored()
    {
        Debug.Log ("Goal!!");
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
}
