using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject SecondCam;
    public bool CameraON = false;
    
    public void CameraMaanager()
    {
        if(CameraON == true)
        {
            Cam1();
            CameraON = false;
        }
        else
        {
            Cam2 ();
            CameraON = true;
        }
    }

    void Cam1()
    {
        MainCam.SetActive(true);
        SecondCam.SetActive(false);
    }

    void Cam2()
    {
        MainCam.SetActive(false);
        SecondCam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
