using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D _Ridgy;
    public float speed = 100.0f;

    private void Awake()
    {
        _Ridgy = GetComponent<Rigidbody2D>();

    }
}
