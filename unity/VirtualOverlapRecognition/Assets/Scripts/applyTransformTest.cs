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

        Debug.Log("Base: " + BaseAxis.transform.eulerAngles.z);
        Debug.Log("Shoulder: " + ShoulderAxis.transform.eulerAngles.z);
        Debug.Log("Elbow: " + ElbowAxis.transform.eulerAngles.z);

    }

    // Update is called once per frame
    void Update()
    {
        // BaseAxis
        if(BaseAxis.transform.eulerAngles.y < 50 && BaseAxis.transform.eulerAngles.y >= 0)
        {
            BaseAxis.transform.eulerAngles += new Vector3(0, .5f, 0);
        }
        else
        {
            BaseAxis.transform.eulerAngles -= new Vector3(0, 0, 0);     // do not move
        }

        // ShoulderAxis
        if (ShoulderAxis.transform.eulerAngles.z < 72 && ShoulderAxis.transform.eulerAngles.z >= 21)
        {
            ShoulderAxis.transform.eulerAngles += new Vector3(0, 0, .5f);
        }
        else
        {
            ShoulderAxis.transform.eulerAngles -= new Vector3(0, 0, 0);     // do not move
        }

        // ElbowAxis
        Debug.Log("Elbow.z: " + ElbowAxis.transform.eulerAngles.z);
        if (ElbowAxis.transform.eulerAngles.z <= 322 && ElbowAxis.transform.eulerAngles.z > 322-50)
        {
            ElbowAxis.transform.eulerAngles += new Vector3(0, 0, 0.5f);
        }
        else
        {
            ElbowAxis.transform.eulerAngles -= new Vector3(0, 0, 0);     // do not move
        }

    }
}
