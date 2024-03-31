using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D _Ridgy;
    private RectTransform _RidgyPos;
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
    void OnCollisionEnter2D(Collision2D boink)    
    {
        if (boink.gameObject.CompareTag("Wall"))
        {
            if (boink.gameObject.transform.position.y > 0)
            {
                //(boink.gameObject.transform.position.y + 50)
                Debug.Log ("My head");
                // _Ridgy.velocity = Vector2.zero;
                // transform.localPosition = new Vector2 (transform.localPosition.x, 422);
            }
            else
            {
                Debug.Log ("My butt");

                // _Ridgy.velocity = Vector2.zero;
                // transform.localPosition = new Vector2 (transform.localPosition.x, -422);
            }
        }
    }
    private void Update()
    {

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
