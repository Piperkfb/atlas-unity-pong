using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _ridgy; 
    private RectTransform _ridgypos;
    public float speed = 10.0f;
    private Vector2 direction;
    private void Awake()
    {
        _ridgy = GetComponent<Rigidbody2D>();
        _ridgypos = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AddStartingForce();
    }

    private void AddStartingForce()
    {
        float x = -1.0f;
        //float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        direction = new Vector2(x,y);

        _ridgy.AddForce(direction * this.speed);
    }

    protected void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Paddle"))
        {
            Paddle paddle = boink.gameObject.GetComponent<Paddle>();
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
            _ridgy.velocity = Vector2.zero;
            _ridgy.AddForce(direction * this.speed);
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
}
