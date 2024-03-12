using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _ridgy; 
    private RectTransform _ridgypos;
    public float speed = 10.0f;
    private Vector2 direction;
    public GameObject scoreMenu;
    public TMP_Text leftScore;
    public TMP_Text rightScore;
    private float leftScoreNumber = 0;
    private float rightScoreNumber = 0;
    public TMP_Text whoScored;
    private Vector2 BallResetPos;
    
    private void Awake()
    {
        _ridgy = GetComponent<Rigidbody2D>();
        _ridgypos = GetComponent<RectTransform>();
        BallResetPos = _ridgypos.transform.position;
        
    }

    private void Start()
    {
        AddStartingForce();
        
    }
    // protected void startingPositions()
    // {
    //     GameObject[] allpaddles = GameObject.FindGameObjectsWithTag("Paddle");
    //     foreach(GameObject paddles in allpaddles)
    //     {
    //         Paddle padd = paddles.GetComponent<Paddle>();

    //     }
    // }

    private void AddStartingForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        direction = new Vector2(x,y);

        _ridgy.AddForce(direction * this.speed);
    }
    private void Update()
    {
        if (leftScoreNumber == 11 || rightScoreNumber == 11)
        {
            Debug.Log ("You win! wow");
        }
    }

    protected void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Paddle"))
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
            _ridgy.AddForce(d * this.speed);
        } 
        if (boink.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y;
            direction.x = -direction.x;
            _ridgy.velocity = Vector2.zero;
            _ridgy.AddForce(direction * this.speed);
        }
        if (boink.gameObject.CompareTag("Goal"))
        {
            Debug.Log ("Goal!!");
            _ridgy.velocity = Vector2.zero;
            if (boink.gameObject.transform.position.x > 0)
            {
                leftScoreNumber++;
                leftScore.text = $"{leftScoreNumber}";
                whoScored.text = $"P1";
            }
            else if (boink.gameObject.transform.position.x < 0)
            {
                rightScoreNumber++;
                rightScore.text = $"{rightScoreNumber}";
                whoScored.text = $"P2";
            }
            scoreMenu.SetActive(true);
            GameObject[] allpaddles = GameObject.FindGameObjectsWithTag("Paddle");
            foreach(GameObject paddles in allpaddles)
            {
                Rigidbody2D padrid = paddles.GetComponent<Rigidbody2D>();
                padrid.constraints = RigidbodyConstraints2D.FreezePosition;
            }
            Invoke("ResetPos", 2.0f);
            Invoke("AddStartingForce", 3.0f);
            
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
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
    private void ResetPos()
    {
        GameObject[] allpaddles = GameObject.FindGameObjectsWithTag("Paddle");
        foreach(GameObject paddles in allpaddles)
            {
                Rigidbody2D padrid = paddles.GetComponent<Rigidbody2D>();
                padrid.constraints = RigidbodyConstraints2D.None;
            }
        _ridgypos.transform.position = BallResetPos;
        scoreMenu.SetActive(false);
    }
}
