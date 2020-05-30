using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyTransformTest : MonoBehaviour
{
    Transform BaseAxis;
    Transform ShoulderAxis;
    Transform ElbowAxis;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Printing someting now ... ");
        
        BaseAxis = transform.Find("BaseAxis");
        ShoulderAxis = BaseAxis.transform.Find("ShoulderAxis");
        ElbowAxis = ShoulderAxis.transform.Find("ElbowAxis");

        Debug.Log("Global Base: " + BaseAxis.transform.eulerAngles.y);
        Debug.Log("Local Base: " + BaseAxis.transform.localEulerAngles.y);

        Debug.Log("Global Shoulder: " + ShoulderAxis.transform.eulerAngles.z);
        Debug.Log("Local Shoulder: " + ShoulderAxis.transform.localEulerAngles.z);

        Debug.Log("Global Elbow: " + ElbowAxis.transform.eulerAngles.z);
        Debug.Log("Local Elbow: " + ElbowAxis.transform.localEulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        // BaseAxis
        if(BaseAxis.transform.localEulerAngles.y < 50 && BaseAxis.transform.localEulerAngles.y >= 0)
        {
            BaseAxis.transform.localEulerAngles += new Vector3(0, .5f, 0);
        }
        else
        {
            BaseAxis.transform.localEulerAngles -= new Vector3(0, 0, 0);     // do not move
        }

        // ShoulderAxis
        if (ShoulderAxis.transform.localEulerAngles.z < 72 && ShoulderAxis.transform.localEulerAngles.z >= 21)
        {
            ShoulderAxis.transform.localEulerAngles += new Vector3(0, 0, .5f);
        }
        else
        {
            ShoulderAxis.transform.localEulerAngles -= new Vector3(0, 0, 0);     // do not move
        }

        // ElbowAxis
        Debug.Log("Elbow.z: " + ElbowAxis.transform.localEulerAngles.z);
        if (ElbowAxis.transform.localEulerAngles.z <= 300 && ElbowAxis.transform.localEulerAngles.z > 250)
        {
            ElbowAxis.transform.localEulerAngles -= new Vector3(0, 0, 0.5f);
        }
        else
        {
            ElbowAxis.transform.localEulerAngles -= new Vector3(0, 0, 0);     // do not move
        }

    }
}
