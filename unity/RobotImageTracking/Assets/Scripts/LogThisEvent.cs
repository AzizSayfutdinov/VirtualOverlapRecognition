using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Microsoft.MixedReality.Toolkit;
using System.Linq;
using System;

public class LogThisEvent : MonoBehaviour
{
    public string savePath;
    private string fileNamefromDoNotDestroy;

    public string fullFilePath;

    public string currentGaze = null;
    public string previousGaze = null;

    public float TimeBegin;
    public float TimeEnd;
    public float difference;
    private string A;

    private void Awake()
    {
        if (GameObject.Find("DONOTDESTROY") != null)
        {
            fileNamefromDoNotDestroy = GameObject.Find("DONOTDESTROY").GetComponent<DoNotDestoryOnLoad>().fileName;
        }
        else
        {
            fileNamefromDoNotDestroy = System.DateTime.Now.ToString("yyyy-MM-dd") + "__" + System.DateTime.Now.ToString("HH-mm-ss") + "_opensight_Log_NOT STARTED FROM BEGINNING" + ".txt";
        }

        // öffentlicher Speicherpfad angeben
        savePath = Application.persistentDataPath + "/opensightLog/";
        Debug.Log("persistentDataPath: " + savePath);

        fullFilePath = savePath + fileNamefromDoNotDestroy;

        //schaut nach, ob die der Pfad exisitert und erstellt gegebenen falls den Pfad
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }


        // file wird erstellt, wenn es nicht schon existiert
        if (!File.Exists(fullFilePath))
        {
            File.WriteAllText(fullFilePath, fileNamefromDoNotDestroy + " - opensight Log" + "\n\n");
            File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "------ App gestartet ------" + "\n");


            // Es wreden die Speicher-Pfade in den Log geschrieben
            File.AppendAllText(fullFilePath, "Application.streamingAssetsPath: " + Application.streamingAssetsPath + "\n");
            File.AppendAllText(fullFilePath, "Application.dataPath: " + Application.dataPath + "\n");
            File.AppendAllText(fullFilePath, "Application.persistentDataPath: " + Application.persistentDataPath + "\n\n");

        }
        else
        {
            File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "--------NEUE SZENE--------" + "\n");
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    void Update()
    {
        if (CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            currentGaze = CoreServices.InputSystem.GazeProvider.GazeTarget.name;
            if (currentGaze != previousGaze && !currentGaze.StartsWith("SpatialMesh"))
            {
                GazeEnd();
                //Debug.Log("GazeEnd");
                GazeBegin();
                //Debug.Log("GazeBegin");
            }

            if (CoreServices.InputSystem.GazeProvider.GazeTarget.name.StartsWith("SpatialMesh"))
            {
                File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "GAZE at RealWorld" + "\n");
            }
        }
        else if (currentGaze != null && !currentGaze.StartsWith("SpatialMesh"))
        {
            currentGaze = null;
            previousGaze = null;
            //Debug.Log("GazeEnd else if");
        }
    }



    void GazeBegin()
    {
        TimeBegin = (float)Time.time;
        //Debug.Log("TimeBegin is: " + TimeBegin + " sec.");
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "Start GAZE at: " + currentGaze + "\n");

        A = currentGaze;
        //Debug.Log("A: " + A);

        previousGaze = currentGaze;
    }

    void GazeEnd()
    {
        TimeEnd = (float)Time.time;
        //Debug.Log("TimeEnd is: " + TimeEnd + " sec.");

        difference = TimeEnd - TimeBegin;
        //Debug.Log("difference: " + difference);

        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "End GAZE: " + A + " finished after " + difference + " sec" + "\n");
    }

 



    // Funktion, um Button zu loggen
    public void LogThisButton(string ButtonName)
    {
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "Button pressed: " + ButtonName + "\n");
    }

    // Funktion, um andere Aktivität zu loggen, auch von anderen Skripten
    public void LogThisActivity(string ActivityEntry)
    {
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + ActivityEntry + "\n");
    }


    // Eye-Kalibrierung erforderlich
    public void EyeCalibrationDetected()
    {
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "Eye-Calibration notwendig" + "\n");
    }

    // Eye-Kalibrierung nicht notwendig
    public void EyeCalibrationNoDetected()
    {
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "Eye-Calibration nicht notwendig" + "\n");
    }

    // HandMenu Logging
    public void HandMenuLogging(string HandMenuEntry)
    {
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "HandMenu: " + HandMenuEntry + "\n");
    }



    // wenn die App beendet wird -> wird bei UWP-Apps nicht ausgelöst
    void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit: jetzt App geschlossen");
        File.AppendAllText(fullFilePath, System.DateTime.Now.ToString("yyyy-MM-dd") + "\t" + System.DateTime.Now.ToString("HH-mm-ss-ms") + "\t" + "------ App geschlossen ------" + "\n");
    }
}
