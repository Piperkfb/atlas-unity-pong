using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D _Ridgy;
    private RectTransform _RidgyPos;
    public float speed = 100.0f;
    private Vector2 ResetPos;

    private void Awake()
    {
        _Ridgy = GetComponent<Rigidbody2D>();
        _RidgyPos = GetComponent<RectTransform>();
        //ResetPos = _RidgyPos.anchoredPosition;        
        ResetPos = _Ridgy.transform.position;
        //ResetPos = _RidgyPos.sizeDelta;
    }
    private void Start()
    {

        
    }
    private void Update()
    {

    }
    protected void OnTriggerEnter2D(Collider2D boink)
    {
        if (boink.gameObject.CompareTag("Wall"))
        {
            Debug.Log ("My head/butt");

            _Ridgy.velocity = Vector2.zero;
        }
    }
    public Vector2 AnchorPos()
    {
        return _RidgyPos.anchoredPosition;
    }
    public Vector2 Resetpos()
    {
        return ResetPos;
    }
}
