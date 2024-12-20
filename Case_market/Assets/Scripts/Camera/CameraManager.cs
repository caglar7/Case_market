

using UnityEngine;
using Cinemachine;
using Template;
using System.Collections.Generic;
using System;
using System.Collections;
using Sirenix.OdinInspector;

public class CameraManager : Singleton<CameraManager>, IModuleInit
{
    public CinemachineBrain cinemachineBrain;
    
    public List<CameraUnit> cameraUnits = new List<CameraUnit>();
    private const int PRIORITY_HIGH = 10;
    private const int PRIORITY_LOW = 0; 

    private CameraAngleType _currentAngleType;
    public CameraAngleType CurrentAngleType => _currentAngleType;


    public void Init() => StartCoroutine(FindCameraUnits());

    public void CutToTarget(CameraAngleType targetAngleType)  
    {
        ChangeAngle(targetAngleType);
    }

    private void ChangeAngle(CameraAngleType targetAngleType)
    {
        foreach (CameraUnit unit in cameraUnits)
        {
            if(unit.angleType == targetAngleType)
            {
                cinemachineBrain.m_DefaultBlend.m_Style = unit.ease;

                cinemachineBrain.m_DefaultBlend.m_Time = unit.easeDuration;

                unit.cam.Priority = PRIORITY_HIGH;

                _currentAngleType = targetAngleType;
            }
            else unit.cam.Priority = PRIORITY_LOW;
        }

        CameraEvents.OnAngleChanged?.Invoke(targetAngleType);
    }

    IEnumerator FindCameraUnits()
    {
        cameraUnits.AddRange(FindObjectsOfType<CameraUnit>());
        yield return 0;
    }
}