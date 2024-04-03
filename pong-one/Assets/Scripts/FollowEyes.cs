using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEyes : MonoBehaviour
{
    public GameHandler GH;
    public Transform Pupil;
    public Transform Ball;
    public float EyeRadius = 1f;
    Vector3 mPupilCenterPos;

    void Start()
    {
        mPupilCenterPos = Pupil.position;
    }


    void Update()
    {
        Ball = GH.newball.GetComponent<Transform>();
        //checks most recent fly spawn as player
            //do it
        Vector3 lookDir = (Ball.position - mPupilCenterPos).normalized;
        Pupil.position = mPupilCenterPos + (lookDir * EyeRadius);
    }
}
