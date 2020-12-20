using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTrajectoryDecreasingError : MonoBehaviour
{
    // Variables
    Transform BaseAxis;
    Transform ShoulderAxis;
    Transform ElbowAxis;
    Transform WristVerticalAxis;

    // Transforms of reference model
    public GameObject BaseAxisRefObj;
    public GameObject ShoulderAxisRefObj;
    public GameObject ElbowAxisRefObj;
    public GameObject WristVerticalAxisRefObj;

    Transform BaseAxisRef;
    Transform ShoulderAxisRef;
    Transform ElbowAxisRef;
    Transform WristVerticalAxisRef;

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

    // trajectory parameters
    int numberOfSteps = 400;

    // errors in deg
    float baseError = 10;
    float shoulderError = 10;
    float elbowError = 10;
    float wristVerticalError = 10;
    float step = 0;

    // error steps
    float baseErrorStep = 2;
    float shoulderErrorStep = 1;
    float elbowErrorStep = 1f;
    float wristVerticalErrorStep = 1;

    int steps = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Find all axis
        BaseAxis = transform.Find("BaseAxis");
        ShoulderAxis = BaseAxis.transform.Find("ShoulderAxis");
        ElbowAxis = ShoulderAxis.transform.Find("ElbowAxis");
        WristVerticalAxis = ElbowAxis.transform.Find("WristVerticalAxis");

        // instantiate ref axis
        BaseAxisRef = BaseAxisRefObj.transform;
        ShoulderAxisRef = ShoulderAxisRefObj.transform;
        ElbowAxisRef = ElbowAxisRefObj.transform;
        WristVerticalAxisRef = WristVerticalAxisRefObj.transform;

        // Set model to initial position
        BaseAxis.transform.localEulerAngles = new Vector3(0, 0, 0);
        ShoulderAxis.transform.localEulerAngles = new Vector3(0, 0, 60);
        ElbowAxis.transform.localEulerAngles = new Vector3(0, 0, -90);
        WristVerticalAxis.transform.localEulerAngles = new Vector3(0, 0, -70);

        // calculate stepSizes 
        baseStep = 180f / numberOfSteps;
        shoulderStep = 120f / numberOfSteps;
        elbowStep = 180f / numberOfSteps;
        wristVerticalStep = 140f / numberOfSteps;

    }

    // Update is called once per frame
    void Update()
    {

        // add error by taking angles of ref model into account
        Vector3 anglesBaseRef = BaseAxisRef.transform.localEulerAngles;
        Vector3 anglesShoulderRef = ShoulderAxisRef.transform.localEulerAngles;
        Vector3 anglesElbowRef = ElbowAxisRef.transform.localEulerAngles;
        Vector3 anglesWristVerticalRef = WristVerticalAxisRef.transform.localEulerAngles;

        // set values
        // rot axis: y
        if(anglesBaseRef.y > 0)
        {
            BaseAxis.transform.localEulerAngles = new Vector3(anglesBaseRef.x, anglesBaseRef.y + baseError, anglesBaseRef.z);
        }
        else
        {
            BaseAxis.transform.localEulerAngles = new Vector3(anglesBaseRef.x, anglesBaseRef.y - baseError, anglesBaseRef.z);
        }
        // rot axis: z
        ShoulderAxis.transform.localEulerAngles = new Vector3(anglesShoulderRef.x, anglesShoulderRef.y, anglesShoulderRef.z + shoulderError);

        // rot axis: z
        ElbowAxis.transform.localEulerAngles = new Vector3(anglesElbowRef.x, anglesElbowRef.y, anglesElbowRef.z + elbowError);

        // rot axis: z
        WristVerticalAxis.transform.localEulerAngles = new Vector3(anglesWristVerticalRef.x, anglesWristVerticalRef.y, anglesWristVerticalRef.z + wristVerticalError);

        // decrease error each period
        if(steps % numberOfSteps == 0)
        {
            if (!(baseError <= 0))
            {
                baseError -= baseErrorStep;
            }
            else
            {
                baseError = 0;
            }
            Debug.Log("baseError: " + baseError);

            if (!(shoulderError <= 0))
            {
                shoulderError -= shoulderErrorStep;
            }
            else
            {
                shoulderError = 0;
            }
            Debug.Log("shoulderError: " + shoulderError);

            if (!(elbowError <= 0))
            {
                elbowError -= elbowErrorStep;
            }
            else
            {
                elbowError = 0;
            }
            Debug.Log("elbowError: " + elbowError);

            if (!(wristVerticalError <= 0))
            {
                wristVerticalError -= wristVerticalErrorStep;
            }
            else
            {
                wristVerticalError = 0;
            }
            Debug.Log("wristVerticalError: " + wristVerticalError);
        }

        steps++;

        //// add error after each period
        //if (steps % (2 * numberOfSteps) == 0)   // war ein bug drin, nochmals probieren
        //{
        //    // decrease error
        //    if (!(baseError < 0))
        //    {
        //        baseError -= baseErrorStep;
        //    }
        //    else
        //    {
        //        baseError = 0;
        //    }
        //    Debug.Log("baseError: " + baseError);
        //    if (!(shoulderError < 0))
        //        shoulderError -= shoulderErrorStep;
        //    if (!(elbowError < 0))
        //        elbowError -= elbowErrorStep;
        //    if (!(wristVerticalError < 0))
        //        wristVerticalError -= wristVerticalErrorStep;

        //    Vector3 currentAngles = BaseAxis.localEulerAngles;
        //    // BaseAxis.transform.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y + baseError, currentAngles.z);
        //    // BaseAxis.transform.localEulerAngles = new Vector3(0, 0 + baseError, 0);

        //    // BaseAxis.transform.localEulerAngles = new Vector3(0, 0 + baseError, 0);
        //    // BaseAxis.transform.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, currentAngles.z);
        //    BaseAxis.transform.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y + baseError, currentAngles.z);


        //    Debug.Log("currentAngles.x: " + currentAngles.x);
        //    Debug.Log("currentAngles.y: " + currentAngles.y);
        //    Debug.Log("currentAngles.z: " + currentAngles.z);
        //    //ShoulderAxis.transform.localEulerAngles = new Vector3(0, 0, 60);
        //    //ElbowAxis.transform.localEulerAngles = new Vector3(0, 0, -90);
        //    //WristVerticalAxis.transform.localEulerAngles = new Vector3(0, 0, -70);

        //}

        //steps++;

    }

    void baseTrajectory()
    {
        // Base from 0° to 180°
        // Debug.Log("Local Base.y: " + (BaseAxis.transform.localEulerAngles.y));

        if (BaseAxis.transform.localEulerAngles.y >= 0 && BaseAxis.transform.localEulerAngles.y <= 180)
        {
            if (baseDir)
            {
                BaseAxis.transform.localEulerAngles += new Vector3(0, baseStep, 0);
                // Change direction if upper limit of 180° is reached
                if (BaseAxis.transform.localEulerAngles.y > 180)
                    baseDir = !baseDir;
            }
            else
            {
                BaseAxis.transform.localEulerAngles -= new Vector3(0, baseStep, 0);
                // Change direction if lower limit of 0° is reached
                // Check < 0 does not work, therefore check if within interval 350-360
                if (BaseAxis.transform.localEulerAngles.y < 360 && BaseAxis.transform.localEulerAngles.y > 360 - tolerance)
                    baseDir = !baseDir;
            }
        }
        else
        {
            if (baseDir)
            {
                BaseAxis.transform.localEulerAngles += new Vector3(0, baseStep, 0);
            }
            else
            {
                BaseAxis.transform.localEulerAngles -= new Vector3(0, baseStep, 0);
            }
        }

        // not easy to start with an error and decrease to 0 over time
        // need to track number of steps taken + current direction of movement + periodicity
        // simple solution: just keep an error for a while
        // then suddenly decrease it

        /*  possible solution: 
            change start value
            after a period, change the error
            track steps / period

        */

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
                ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, shoulderStep);
                // Change direction if upper limit of 60° is reached
                if (ShoulderAxis.transform.localEulerAngles.z > 60 && ShoulderAxis.transform.localEulerAngles.z < 60 + tolerance)
                    shoulderDir = !shoulderDir;
            }
            else
            {
                ShoulderAxis.transform.localEulerAngles -= new Vector3(0, 0, shoulderStep);
                // Change direction if lower limit of -60° is reached
                if (ShoulderAxis.transform.localEulerAngles.z < 300 && ShoulderAxis.transform.localEulerAngles.z > 300 - tolerance)
                    shoulderDir = !shoulderDir;
            }
        }
        else
        {
            if (shoulderDir)
            {
                ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, shoulderStep);
            }
            else
            {
                ShoulderAxis.transform.localEulerAngles -= new Vector3(0, 0, shoulderStep);
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
                ElbowAxis.transform.localEulerAngles += new Vector3(0, 0, elbowStep);
                // Change direction if upper limit of 90° is reached
                if (ElbowAxis.transform.localEulerAngles.z > 90 && ElbowAxis.transform.localEulerAngles.z < 90 + tolerance)
                    elbowDir = !elbowDir;
            }
            else
            {
                ElbowAxis.transform.localEulerAngles -= new Vector3(0, 0, elbowStep);
                // Change direction if lower limit of -90° is reached
                if (ElbowAxis.transform.localEulerAngles.z < 270 && ElbowAxis.transform.localEulerAngles.z > 270 - tolerance)
                    elbowDir = !elbowDir;
            }
        }
        else
        {
            if (elbowDir)
            {
                ElbowAxis.transform.localEulerAngles += new Vector3(0, 0, elbowStep);
            }
            else
            {
                ElbowAxis.transform.localEulerAngles -= new Vector3(0, 0, elbowStep);
            }
        }
    }


    void wristVerticalTrajectory()
    {
        // Shoulder from 70° to -70°
        // Debug.Log("Local Wrist vertical.z: " + (WristVerticalAxis.transform.localEulerAngles.z));
        if (WristVerticalAxis.transform.localEulerAngles.z >= 0 && WristVerticalAxis.transform.localEulerAngles.z <= 70 ||
            WristVerticalAxis.transform.localEulerAngles.z >= 290 && WristVerticalAxis.transform.localEulerAngles.z <= 360)
        {
            if (wristVerticalDir)
            {
                WristVerticalAxis.transform.localEulerAngles += new Vector3(0, 0, wristVerticalStep);
                // Change direction if upper limit of 70° is reached
                if (WristVerticalAxis.transform.localEulerAngles.z > 70 && WristVerticalAxis.transform.localEulerAngles.z < 70 + tolerance)
                    wristVerticalDir = !wristVerticalDir;
            }
            else
            {
                WristVerticalAxis.transform.localEulerAngles -= new Vector3(0, 0, wristVerticalStep);
                // Change direction if lower limit of -70° is reached
                if (WristVerticalAxis.transform.localEulerAngles.z < 290 && WristVerticalAxis.transform.localEulerAngles.z > 290 - tolerance)
                    wristVerticalDir = !wristVerticalDir;
            }
        }
        else
        {
            if (wristVerticalDir)
            {
                WristVerticalAxis.transform.localEulerAngles += new Vector3(0, 0, wristVerticalStep);
            }
            else
            {
                WristVerticalAxis.transform.localEulerAngles -= new Vector3(0, 0, wristVerticalStep);
            }
        }
    }


    void calcTrajectory(Transform axis, RotationalAxis rotationalAxis)
    {
        // get current value
        // add current error
        // set angle calculated angle value
        // decrement error
        // stop decrementing if error is < 0

        Vector3 currentAngles = axis.localEulerAngles;
        if (rotationalAxis == RotationalAxis.xAxis)
        {
            axis.localEulerAngles = new Vector3(currentAngles.x + baseError, currentAngles.y, currentAngles.z);
        }
        else if (rotationalAxis == RotationalAxis.yAxis)
        {
            axis.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y + baseError, currentAngles.z);
        }
        else
        {
            axis.localEulerAngles = new Vector3(currentAngles.x, currentAngles.y, currentAngles.z + baseError);
        }

    }

    enum RotationalAxis
    {
        xAxis,
        yAxis,
        zAxis
    }

}
