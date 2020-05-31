using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        // print global and local values
        Debug.Log("Global angle: " + transform.eulerAngles.z);
        Debug.Log("Local angle: " + transform.localEulerAngles.z);

        if(transform.eulerAngles.z != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
