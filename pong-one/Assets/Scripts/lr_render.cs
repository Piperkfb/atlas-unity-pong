using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_render : MonoBehaviour
{
    private LineRenderer lr;
    public Transform[] points;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        LineSetup(points);
    }

    public void LineSetup(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].localPosition);
        }
    }
}
