using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Paddle
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    private Vector2 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKey(downKey))
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    private void FixedUpdate() 
    {
        if (direction.sqrMagnitude > 0)
        {
            _Ridgy.AddForce(direction * speed);
        }    
    }

}
