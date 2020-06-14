using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IconBehaviour : MonoBehaviour
{
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

    // trajectory parameters
    int numberOfSteps = 400;

    // Base Icon
    public GameObject iconUpperBase;
    public GameObject iconLowerBase;
    Renderer rendererUpperBase;
    Renderer rendererLowerBase;

    // Shoulder Icon
    public GameObject iconUpperShoulder;
    public GameObject iconLowerShoulder;
    Renderer rendererUpperShoulder;
    Renderer rendererLowerShoulder;

    // Elbow Icon
    public GameObject iconUpperElbow;
    public GameObject iconLowerElbow;
    Renderer rendererUpperElbow;
    Renderer rendererLowerElbow;

    // Wrist Vertical Icon
    public GameObject iconUpperWristVertical;
    public GameObject iconLowerWristVertical;
    Renderer rendererUpperWristVertical;
    Renderer rendererLowerWristVertical;


    public Color color = Color.green;

    private float colorIncrease;

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
        WristVerticalAxis.transform.localEulerAngles = new Vector3(0, 0, -70);

        // calculate stepSizes 
        baseStep = 180f / numberOfSteps;
        shoulderStep = 120f / numberOfSteps;
        elbowStep = 180f / numberOfSteps;
        wristVerticalStep = 140f / numberOfSteps;

        colorIncrease  = 2f / numberOfSteps;

        rendererUpperBase = iconUpperBase.GetComponent<Renderer>();
        rendererLowerBase = iconLowerBase.GetComponent<Renderer>();
        rendererUpperShoulder = iconUpperShoulder.GetComponent<Renderer>();
        rendererLowerShoulder = iconLowerShoulder.GetComponent<Renderer>();
        rendererUpperElbow = iconUpperElbow.GetComponent<Renderer>();
        rendererLowerElbow = iconLowerElbow.GetComponent<Renderer>();
        rendererUpperWristVertical = iconUpperWristVertical.GetComponent<Renderer>();
        rendererLowerWristVertical = iconLowerWristVertical.GetComponent<Renderer>();


    }

    // Update is called once per frame
    void Update()
    {
        // renderer.material.color = Color.red;
        baseTrajectory();
        shoulderTrajectory();
        elbowTrajectory();
        wristVerticalTrajectory();
    }

    void baseTrajectory()
    {
        // Base from 0° to 180°
        // Debug.Log("Local Base.y: " + (BaseAxis.transform.localEulerAngles.y));
        Debug.Log("Red: " + color.r);

        if (BaseAxis.transform.localEulerAngles.y >= 0 && BaseAxis.transform.localEulerAngles.y <= 180)
        {
            if (baseDir)
            {
                color.r += colorIncrease;
                
                if(color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperBase.material.color = color;
                rendererLowerBase.material.color = color;

                BaseAxis.transform.localEulerAngles += new Vector3(0, baseStep, 0);
                // Change direction if upper limit of 180° is reached
                if (BaseAxis.transform.localEulerAngles.y > 180)
                    baseDir = !baseDir;
            }
            else
            {
                color.g += colorIncrease;            
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperBase.material.color = color;
                rendererLowerBase.material.color = color;

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
                color.r += colorIncrease;
                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperBase.material.color = color;
                rendererLowerBase.material.color = color;

                BaseAxis.transform.localEulerAngles += new Vector3(0, baseStep, 0);
            }
            else
            {
                color.g += colorIncrease;              
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperBase.material.color = color;
                rendererLowerBase.material.color = color;

                BaseAxis.transform.localEulerAngles -= new Vector3(0, baseStep, 0);
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
                color.r += colorIncrease;

                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperShoulder.material.color = color;
                rendererLowerShoulder.material.color = color;

                ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, shoulderStep);
                // Change direction if upper limit of 60° is reached
                if (ShoulderAxis.transform.localEulerAngles.z > 60 && ShoulderAxis.transform.localEulerAngles.z < 60 + tolerance)
                    shoulderDir = !shoulderDir;
            }
            else
            {
                color.g += colorIncrease;
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperShoulder.material.color = color;
                rendererLowerShoulder.material.color = color;

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
                color.r += colorIncrease;
                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperShoulder.material.color = color;
                rendererLowerShoulder.material.color = color;

                ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, shoulderStep);
            }
            else
            {
                color.g += colorIncrease;
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperShoulder.material.color = color;
                rendererLowerShoulder.material.color = color;

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
                color.r += colorIncrease;

                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperElbow.material.color = color;
                rendererLowerElbow.material.color = color;

                ElbowAxis.transform.localEulerAngles += new Vector3(0, 0, elbowStep);
                // Change direction if upper limit of 90° is reached
                if (ElbowAxis.transform.localEulerAngles.z > 90 && ElbowAxis.transform.localEulerAngles.z < 90 + tolerance)
                    elbowDir = !elbowDir;
            }
            else
            {
                color.g += colorIncrease;
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperWristVertical.material.color = color;
                rendererLowerElbow.material.color = color;

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
                color.r += colorIncrease;
                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperElbow.material.color = color;
                rendererLowerElbow.material.color = color;

                ElbowAxis.transform.localEulerAngles += new Vector3(0, 0, elbowStep);
            }
            else
            {
                color.g += colorIncrease;
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperElbow.material.color = color;
                rendererLowerElbow.material.color = color;

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
                color.r += colorIncrease;

                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperWristVertical.material.color = color;
                rendererLowerWristVertical.material.color = color;

                WristVerticalAxis.transform.localEulerAngles += new Vector3(0, 0, wristVerticalStep);
                // Change direction if upper limit of 70° is reached
                if (WristVerticalAxis.transform.localEulerAngles.z > 70 && WristVerticalAxis.transform.localEulerAngles.z < 70 + tolerance)
                    wristVerticalDir = !wristVerticalDir;
            }
            else
            {
                color.g += colorIncrease;
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperWristVertical.material.color = color;
                rendererLowerWristVertical.material.color = color;

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
                color.r += colorIncrease;
                if (color.r >= 1)
                    color.g -= colorIncrease;

                // apply color
                rendererUpperWristVertical.material.color = color;
                rendererLowerWristVertical.material.color = color;

                WristVerticalAxis.transform.localEulerAngles += new Vector3(0, 0, wristVerticalStep);
            }
            else
            {
                color.g += colorIncrease;
                if (color.r >= 1)
                    color.r -= colorIncrease;

                // apply color
                rendererUpperWristVertical.material.color = color;
                rendererLowerWristVertical.material.color = color;

                WristVerticalAxis.transform.localEulerAngles -= new Vector3(0, 0, wristVerticalStep);
            }
        }
    }
}
