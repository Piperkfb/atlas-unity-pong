using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _ridgy; 
    public float speed = 10.0f;
    private Vector2 direction;
    private void Awake()
    {
        _ridgy = GetComponent<Rigidbody2D>();
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
            direction.x = -direction.x * 2;
            direction.y = -direction.y * 2;
            _ridgy.AddForce(direction * this.speed);
        } 
        if (boink.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y * 2;
            _ridgy.AddForce(direction * this.speed);
        }

    }
}
