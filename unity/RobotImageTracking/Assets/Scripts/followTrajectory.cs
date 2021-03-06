﻿using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTrajectory : MonoBehaviour
{
    // public
    public bool start = true;

    // Variables
    Transform BaseAxis;
    Transform ShoulderAxis;
    Transform ElbowAxis;
    Transform WristVerticalAxis;

    // Directions: true - increases angle & false - decreases angle
    bool baseDir = true;
    bool shoulderDir = false;
    bool elbowDir = true;
    bool wristVerticalDir = true;

    // stepsizes: amount of increase of angle each frame
    float baseStep = 0.5f;
    float shoulderStep = 1;
    float elbowStep = 1;
    float wristVerticalStep = 1;

    // angle tolerance when checking for euwality
    int tolerance = 10;     // degree

    // angle of movement for one period (3.6s)
    float baseAngle = 180f;
    float shoulderAngle = 120f;
    float elbowAngle = 180f;
    float wristVerticalAngle = 180f;

    // angle of movement for 1 sec
    float baseAngleSec;
    float shoulderAngleSec;
    float elbowAngleSec;
    float wristVerticalAngleSec;

    float durationOfRobotPeriod = 3.65f;

    // trajectory parameters
    // using Time.deltaTime
    // robot movement duration for 1 period: ~3.6s
    // number of steps in one period can be defined: 198
    // number of steps in 1 sec: 198/3.6 = 55 

    int numberOfSteps = 55;        
                                   
    int numberOfPauseSteps = 0;    // short pause of motion after each period

    int steps = 0;
    int pauseSteps = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Find all axis
        BaseAxis = transform.Find("BaseAxis");
        ShoulderAxis = BaseAxis.transform.Find("ShoulderAxis");
        ElbowAxis = ShoulderAxis.transform.Find("ElbowAxis");   
        WristVerticalAxis = ElbowAxis.transform.Find("WristVerticalAxis");

        // Set model to initial position
        BaseAxis.transform.localEulerAngles = new Vector3(0, 0, 0);
        ShoulderAxis.transform.localEulerAngles = new Vector3(0, 0, 60);
        ElbowAxis.transform.localEulerAngles = new Vector3(0, 0, -90);
        WristVerticalAxis.transform.localEulerAngles = new Vector3(0, 0, -90);

        // calculate stepSizes 
        baseStep = 180f / numberOfSteps;
        shoulderStep = 120f / numberOfSteps;
        elbowStep = 180f / numberOfSteps;
        wristVerticalStep = 180f / numberOfSteps;

        // angle of movement for 1 sec
        baseAngleSec = baseAngle / durationOfRobotPeriod;
        shoulderAngleSec = shoulderAngle / durationOfRobotPeriod;
        elbowAngleSec = elbowAngle / durationOfRobotPeriod;
        wristVerticalAngleSec = wristVerticalAngle / durationOfRobotPeriod;

    }

    // Update is called once per frame
    void Update()
    {
        // the runtime of trajectory is 4s: TODO: calculate needed stepsize
        // Base from 0° to 180°

        // short pause after each period
        while(pauseSteps > 0)
        {
            pauseSteps--;
            return;
        }

        baseTrajectory();
        shoulderTrajectory();
        elbowTrajectory();
        wristVerticalTrajectory();

    }

    void baseTrajectory()
    {
        // Base from 0° to 180°
        // Debug.Log("Local Base.y: " + (BaseAxis.transform.localEulerAngles.y));

        if (BaseAxis.transform.localEulerAngles.y >= 0 && BaseAxis.transform.localEulerAngles.y <= 180)
        {
            if (baseDir)
            {
                BaseAxis.transform.localEulerAngles += new Vector3(0, baseAngleSec, 0) * Time.deltaTime;
                // Change direction if upper limit of 180° is reached
                if (BaseAxis.transform.localEulerAngles.y > 180)
                {
                    baseDir = !baseDir;
                    pauseSteps = numberOfPauseSteps;
                }

            }
            else
            {
                BaseAxis.transform.localEulerAngles -= new Vector3(0, baseAngleSec, 0) * Time.deltaTime;
                // Change direction if lower limit of 0° is reached
                // Check < 0 does not work, therefore check if within interval 350-360
                if (BaseAxis.transform.localEulerAngles.y < 360 && BaseAxis.transform.localEulerAngles.y > 360 - tolerance)
                {
                    baseDir = !baseDir;
                    pauseSteps = numberOfPauseSteps;
                }
            }
        }
        else
        {
            if (baseDir)
            {
                BaseAxis.transform.localEulerAngles += new Vector3(0, baseAngleSec, 0) * Time.deltaTime;
            }
            else
            {
                BaseAxis.transform.localEulerAngles -= new Vector3(0, baseAngleSec, 0) * Time.deltaTime;
            }
        }
    }

    void shoulderTrajectory()
    {
        // Shoulder from 60° to -60°
        // Debug.Log("Local Shoulder.z: " + (ShoulderAxis.transform.localEulerAngles.z));
        if (ShoulderAxis.transform.localEulerAngles.z >= 0 && ShoulderAxis.transform.localEulerAngles.z <= 60 ||
            ShoulderAxis.transform.localEulerAngles.z >= 300 && ShoulderAxis.transform.localEulerAngles.z <= 360)
        {
            if (shoulderDir)
            {
                ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, shoulderAngleSec) * Time.deltaTime;
                // Change direction if upper limit of 60° is reached
                if (ShoulderAxis.transform.localEulerAngles.z > 60 && ShoulderAxis.transform.localEulerAngles.z < 60 + tolerance)
                    shoulderDir = !shoulderDir;
            }
            else
            {
                ShoulderAxis.transform.localEulerAngles -= new Vector3(0, 0, shoulderAngleSec) * Time.deltaTime;
                // Change direction if lower limit of -60° is reached
                if (ShoulderAxis.transform.localEulerAngles.z < 300 && ShoulderAxis.transform.localEulerAngles.z > 300 - tolerance)
                    shoulderDir = !shoulderDir;
            }
        }
        else
        {
            if (shoulderDir)
            {
                ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, shoulderAngleSec) * Time.deltaTime;
            }
            else
            {
                ShoulderAxis.transform.localEulerAngles -= new Vector3(0, 0, shoulderAngleSec) * Time.deltaTime;
            }
        }
    }

    void elbowTrajectory()
    {
        // Shoulder from 90° to -90°
        // Debug.Log("Local Elbow.z: " + (ElbowAxis.transform.localEulerAngles.z));
        if (ElbowAxis.transform.localEulerAngles.z >= 0 && ElbowAxis.transform.localEulerAngles.z <= 90 ||
            ElbowAxis.transform.localEulerAngles.z >= 270 && ElbowAxis.transform.localEulerAngles.z <= 360)
        {
            if (elbowDir)
            {
                ElbowAxis.transform.localEulerAngles += new Vector3(0, 0, elbowAngleSec) * Time.deltaTime;
                // Change direction if upper limit of 90° is reached
                if (ElbowAxis.transform.localEulerAngles.z > 90 && ElbowAxis.transform.localEulerAngles.z < 90 + tolerance)
                    elbowDir = !elbowDir;
            }
            else
            {
                ElbowAxis.transform.localEulerAngles -= new Vector3(0, 0, elbowAngleSec) * Time.deltaTime;
                // Change direction if lower limit of -90° is reached
                if (ElbowAxis.transform.localEulerAngles.z < 270 && ElbowAxis.transform.localEulerAngles.z > 270 - tolerance)
                    elbowDir = !elbowDir;
            }
        }
        else
        {
            if (elbowDir)
            {
                ElbowAxis.transform.localEulerAngles += new Vector3(0, 0, elbowAngleSec) * Time.deltaTime;
            }
            else
            {
                ElbowAxis.transform.localEulerAngles -= new Vector3(0, 0, elbowAngleSec) * Time.deltaTime;
            }
        }
    }


    void wristVerticalTrajectory()
    {
        // Shoulder from 70° to -70°
        double limitAngle = 90;
        // Debug.Log("Local Wrist vertical.z: " + (WristVerticalAxis.transform.localEulerAngles.z));
        if (WristVerticalAxis.transform.localEulerAngles.z >= 0 && WristVerticalAxis.transform.localEulerAngles.z <= limitAngle ||
            WristVerticalAxis.transform.localEulerAngles.z >= 360 - limitAngle && WristVerticalAxis.transform.localEulerAngles.z <= 360)
        {
            if (wristVerticalDir)
            {
                WristVerticalAxis.transform.localEulerAngles += new Vector3(0, 0, wristVerticalAngleSec) * Time.deltaTime;
                // Change direction if upper limit of 70° is reached
                if (WristVerticalAxis.transform.localEulerAngles.z > limitAngle && WristVerticalAxis.transform.localEulerAngles.z < limitAngle + tolerance)
                    wristVerticalDir = !wristVerticalDir;
            }
            else
            {
                WristVerticalAxis.transform.localEulerAngles -= new Vector3(0, 0, wristVerticalAngleSec) * Time.deltaTime;
                // Change direction if lower limit of -70° is reached
                if (WristVerticalAxis.transform.localEulerAngles.z < 360 - limitAngle && WristVerticalAxis.transform.localEulerAngles.z > 360 - limitAngle - tolerance)
                    wristVerticalDir = !wristVerticalDir;
            }
        }
        else
        {
            if (wristVerticalDir)
            {
                WristVerticalAxis.transform.localEulerAngles += new Vector3(0, 0, wristVerticalAngleSec) * Time.deltaTime;
            }
            else
            {
                WristVerticalAxis.transform.localEulerAngles -= new Vector3(0, 0, wristVerticalAngleSec) * Time.deltaTime;
            }
        }
    }


}
