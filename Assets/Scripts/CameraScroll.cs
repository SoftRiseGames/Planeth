using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] int CameraStartNumber = 1;
    public List<CinemachineVirtualCamera> allCameras;

    private void Start()
    {
        allCameras[CameraStartNumber].gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        Controlls.isNext += NextCamera;
        Controlls.isPrevious += PreviousCamera;
    }
    private void OnDisable()
    {
        
        Controlls.isNext -= NextCamera;
        Controlls.isPrevious -= PreviousCamera;
        
    }

    private void NextCamera()
    {
        if (CameraStartNumber < allCameras.Count-1)
        {

            allCameras[CameraStartNumber].gameObject.SetActive(false);
            CameraStartNumber = CameraStartNumber + 1;
            allCameras[CameraStartNumber].gameObject.SetActive(true);
        }
       
    }
    private void PreviousCamera()
    {
        if (CameraStartNumber > 0)
        {
            allCameras[CameraStartNumber].gameObject.SetActive(false);
            CameraStartNumber = CameraStartNumber - 1;
            allCameras[CameraStartNumber].gameObject.SetActive(true);
        }
       
        
    }
}
