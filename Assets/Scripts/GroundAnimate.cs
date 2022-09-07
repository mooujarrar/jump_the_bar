using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundAnimate : MonoBehaviour
{
    // Events to send to other components
    public static GroundAnimate current;



    public int roundTime;
    private int actualTime;

    private bool allowRotation = false;

    public event Action<int> onNewRoundTime;

    private void Awake()
    {
        current = this;
    }

    void Start()
    {
        actualTime = 0;
        AnimationAndMovementController.current.onGroundRotationStart += OnStartRotating;
    }

    void Update()
    {
        if(allowRotation)
        {
            // Spin the object around the target at 20 degrees/second.
            transform.RotateAround(gameObject.transform.position, Vector3.up, getActualVelocity() * Time.deltaTime);
        }

    }

    private void OnStartRotating()
    {
        allowRotation = true;
        StartCoroutine(countSeconds());
    }

    IEnumerator countSeconds()
    {
        while(actualTime < roundTime)
        {
            actualTime++;
            yield return new WaitForSeconds(1f);
        }
    }

    public float getActualVelocity()
    {
        return 40.0f;
        //return ((targetedSecondsPerRound - initialSecondsPerRound) / (float)roundTime) * actualTime + initialSecondsPerRound;
    }
}
