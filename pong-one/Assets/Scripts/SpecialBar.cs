using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public GameHandler GH;
    public GameObject SPLeft;
    public GameObject SPRight;
    public Sprite S0, S1, S2, S3, S4, S5;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GH.specialBarLeft == 0)
        {
            SPLeft.GetComponent<Image>().sprite = S0;
        }
        else if (GH.specialBarLeft == 1)
        {
            SPLeft.GetComponent<Image>().sprite = S1;
        }
        else if (GH.specialBarLeft == 2)
        {
            SPLeft.GetComponent<Image>().sprite = S2;
        }
        else if (GH.specialBarLeft == 3)
        {
            SPLeft.GetComponent<Image>().sprite = S3;
        }
        else if (GH.specialBarLeft == 4)
        {
            SPLeft.GetComponent<Image>().sprite = S4;
        }
        else if (GH.specialBarLeft == 5)
        {
            SPLeft.GetComponent<Image>().sprite = S5;
        }

        if (GH.specialBarRight == 0)
        {
            SPRight.GetComponent<Image>().sprite = S0;
        }
        else if (GH.specialBarRight == 1)
        {
            SPRight.GetComponent<Image>().sprite = S1;
        }
        else if (GH.specialBarRight == 2)
        {
            SPRight.GetComponent<Image>().sprite = S2;
        }
        else if (GH.specialBarRight == 3)
        {
            SPRight.GetComponent<Image>().sprite = S3;
        }
        else if (GH.specialBarRight == 4)
        {
            SPRight.GetComponent<Image>().sprite = S4;
        }
        else if (GH.specialBarRight == 5)
        {
            SPRight.GetComponent<Image>().sprite = S5;
        }
    }
    
}
