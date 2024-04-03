using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private lr_render line;
    // Start is called before the first frame update
    void Start()
    {
        line.LineSetup(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
