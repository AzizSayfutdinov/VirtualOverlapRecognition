using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestoryOnLoad : MonoBehaviour
{
    public string fileName = null;



    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


        // Filename global speichern
        if (fileName == "")
        {
            fileName = System.DateTime.Now.ToString("yyyy-MM-dd") + "__" + System.DateTime.Now.ToString("HH-mm-ss") + "_opensight_Log" + ".txt";
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
