using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGScroll : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float x, y;
    [SerializeField] private float speed;

    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + new Vector2(x, y) * Time.deltaTime * speed, _img.uvRect.size);
    }
}
