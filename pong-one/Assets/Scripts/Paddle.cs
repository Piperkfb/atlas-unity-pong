using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        ResetPos = _Ridgy.transform.localPosition;
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
            
            if (boink.gameObject.transform.position.y > 0)
            {
                //(boink.gameObject.transform.position.y + 50)
                Debug.Log ("My head/butt");
                _Ridgy.velocity = Vector2.zero;
                transform.localPosition = new Vector2 (transform.localPosition.x, 600);
            }
            else
            {
                _Ridgy.velocity = Vector2.zero;
                transform.localPosition = new Vector2 (transform.localPosition.x, -600);
            }
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
